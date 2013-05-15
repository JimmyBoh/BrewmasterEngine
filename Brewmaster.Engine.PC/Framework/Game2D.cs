using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BrewmasterEngine.Debugging;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.Framework
{
    public abstract class Game2D : Game
    {
        #region Constructor

        protected Game2D()
        {
            CurrentGame.SetGame(this);

#if WINDOWS || LINUX || MAC
            ScreenWidth = 1024;
            ScreenHeight = 800;
#else
            ScreenHeight = Window.ClientBounds.Height;
            ScreenWidth = Window.ClientBounds.Width;
#endif
            ContentRoot = "Content";
            DefaultFontPath = string.Empty;
            Fullscreen = false;
            DebugMode = false;

            IsMouseVisible = true;

            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = ContentRoot;
        }

        #endregion

        #region Properties

        public GraphicsDeviceManager Graphics { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }

        public string ContentRoot { get; set; }
        public string DefaultFontPath { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public bool Fullscreen { get; set; }
        public Color BackgroundColor { get; set; }
        public bool DebugMode { get; set; }

        public abstract IEnumerable<string> PreloadTextures { get; }
        public abstract IEnumerable<string> PreloadFonts { get; }

        public SceneManager SceneManager { get; set; }


        private Dictionary<string, GameObject> backgroundObjects;
        public abstract IEnumerable<GameObject> BackgroundObjects { get; }

        private Dictionary<string, GameObject> foregroundObjects; 
        public abstract IEnumerable<GameObject> ForegroundObjects { get; }

        #endregion

        #region Abstract Methods

        public abstract void Init();
        public abstract IEnumerable<Scene> Scenes { get; }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            #region XNA Setup

            if (ScreenWidth > 0)
                Graphics.PreferredBackBufferWidth = ScreenWidth;
            if (ScreenHeight > 0)
                Graphics.PreferredBackBufferHeight = ScreenHeight;

            this.IsFixedTimeStep = false;
            Graphics.SynchronizeWithVerticalRetrace = false;
            Graphics.ApplyChanges();

            #endregion

            #region Manager Setup

            SceneManager = new SceneManager();
            SceneManager.AddScenes(Scenes);

            #endregion

            Init();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            if (PreloadTextures != null)
                Preload<Texture2D>(PreloadTextures.ToArray());
            if (PreloadFonts != null)
                Preload<SpriteFont>(PreloadFonts.ToArray());

            backgroundObjects = new Dictionary<string, GameObject>();
            foreach (var obj in BackgroundObjects)
                backgroundObjects.Add(obj.Name, obj);

            foregroundObjects = new Dictionary<string, GameObject>();
            foreach (var obj in ForegroundObjects)
                foregroundObjects.Add(obj.Name, obj);

            SceneManager.LoadDefaultScene();
        }

        protected override void UnloadContent()
        {
            SceneManager.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            var bgKeys = backgroundObjects.Keys;
            foreach (var k in bgKeys)
                backgroundObjects[k].Update(gameTime);

            SceneManager.Update(gameTime);

            var fgKeys = foregroundObjects.Keys;
            foreach (var k in fgKeys)
                foregroundObjects[k].Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(BackgroundColor);
            GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            SpriteBatch.Begin();

            // Draw backround objects...
            var keys = backgroundObjects.Keys;
            foreach (var k in keys)
                backgroundObjects[k].Draw(gameTime);

            // Draw current scene...
            SceneManager.Draw(gameTime);

            // Draw foreground objects.
            var fgKeys = foregroundObjects.Keys;
            foreach (var k in fgKeys)
                foregroundObjects[k].Draw(gameTime);

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

        #region Helpers

        public void Preload<T>(string[] assets)
        {
            foreach (var asset in assets)
            {
                try
                {
                    Content.Load<T>(asset);
                }
                catch
                {
                    Debugger.Log("! Failed to preload Texture2D[{0}]...", asset);
                }
            }
        }

        #endregion
    }
}
