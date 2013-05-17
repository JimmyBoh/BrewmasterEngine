using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BrewmasterEngine.Debugging;
using BrewmasterEngine.Extensions;
using BrewmasterEngine.Graphics;
using BrewmasterEngine.Graphics.Content;
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

            if (ScreenWidth > 0)
                Graphics.PreferredBackBufferWidth = ScreenWidth;
            if (ScreenHeight > 0)
                Graphics.PreferredBackBufferHeight = ScreenHeight;

            this.IsFixedTimeStep = false;
            Graphics.SynchronizeWithVerticalRetrace = false;
            Graphics.ApplyChanges();
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


        private PriorityDictionary<string, GameObject, int> backgroundObjects;
        public abstract IEnumerable<GameObject> BackgroundObjects { get; }

        private PriorityDictionary<string, GameObject, int> foregroundObjects; 
        public abstract IEnumerable<GameObject> ForegroundObjects { get; }

        #endregion

        #region Abstract Methods

        public abstract void Init();
        public abstract IEnumerable<Scene> Scenes { get; }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            CurrentGame.Window.SetInitialSize();

            SceneManager = new SceneManager();
            SceneManager.AddScenes(Scenes);

            Init();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            if (PreloadTextures != null)
                ContentHandler.Preload<Texture2D>(PreloadTextures);
            if (PreloadFonts != null)
                ContentHandler.Preload<SpriteFont>(PreloadFonts);

            backgroundObjects = new PriorityDictionary<string, GameObject, int>(o => o.ZIndex);
            foreach (var obj in BackgroundObjects)
                backgroundObjects.Add(obj.Name, obj);

            foregroundObjects = new PriorityDictionary<string, GameObject, int>(o => o.ZIndex);
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
            // Update the background objects.
            backgroundObjects.ForEach(o => o.Update(gameTime));

            // Update the current scene.
            SceneManager.Update(gameTime);

            // Update the foreground objects.
            foregroundObjects.ForEach(o => o.Update(gameTime));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(BackgroundColor);
            GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            SpriteBatch.Begin();

            // Draw backround objects...
            backgroundObjects.ForEach(o => o.Draw(gameTime));

            // Draw current scene...
            SceneManager.Draw(gameTime);

            // Draw foreground objects.
            foregroundObjects.ForEach(o => o.Draw(gameTime));

            SpriteBatch.End();

            base.Draw(gameTime); 
        }

        #endregion
    }
}
