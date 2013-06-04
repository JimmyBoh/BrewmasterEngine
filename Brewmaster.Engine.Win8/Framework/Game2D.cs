using System;
using System.Collections.Generic;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace BrewmasterEngine.Framework
{
    public abstract class Game2D : Game
    {
        #region Constructor

        protected Game2D()
        {
            Graphics = new GraphicsDeviceManager(this);
            CurrentGame.SetGame(this);

#if WINDOWS || LINUX || MAC
            ScreenWidth = 1366;
            ScreenHeight = 768;
#else
            ScreenWidth = Window.ClientBounds.Width;
            ScreenHeight = Window.ClientBounds.Height;
#endif

            ContentRoot = "Content";
            DefaultFontPath = string.Empty;
            Fullscreen = false;
            DebugMode = false;

            IsMouseVisible = true;

            Content.RootDirectory = ContentRoot;

            if (ScreenWidth > 0)
                Graphics.PreferredBackBufferWidth = ScreenWidth;
            if (ScreenHeight > 0)
                Graphics.PreferredBackBufferHeight = ScreenHeight;

            this.IsFixedTimeStep = false;
            Graphics.SynchronizeWithVerticalRetrace = false;
            Graphics.ApplyChanges();
            CurrentGame.WindowSize = Window.ClientBounds.GetSize();
            

#if NETFX_CORE
            Window.ClientSizeChanged += checkSnapOnResize;
#endif
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

        public abstract GestureType EnabledGestures { get; } 

        public SceneManager SceneManager { get; private set; }

        private GameObjectCollection backgroundObjects;
        public abstract IEnumerable<GameObject> BackgroundObjects { get; }

        private GameObjectCollection foregroundObjects; 
        public abstract IEnumerable<GameObject> ForegroundObjects { get; }

        #endregion

        #region Abstract Methods

        public virtual void Init(){ }
        public abstract IEnumerable<Scene> Scenes { get; }
        public abstract void PreloadContent();

        #endregion

        #region Methods

        protected override void Initialize()
        {
            CurrentGame.Window.SetInitialSize();

            SceneManager = new SceneManager();
            SceneManager.AddScenes(Scenes);

            TouchPanel.EnableMouseTouchPoint = true;
            TouchPanel.EnableMouseGestures = true;
            TouchPanel.EnabledGestures = EnabledGestures;

            Init();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            PreloadContent();

            backgroundObjects = new GameObjectCollection();
            foreach (var obj in BackgroundObjects)
                backgroundObjects.Add(obj);

            foregroundObjects = new GameObjectCollection();
            foreach (var obj in ForegroundObjects)
                foregroundObjects.Add(obj);

            SceneManager.LoadDefaultScene();
        }

        protected override void UnloadContent()
        {
            SceneManager.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            // Update the global variables in CurrentGame...
            CurrentGame.GameTime = gameTime;
            
            // Update touch locations.
            CurrentGame.TouchState = TouchPanel.GetState();

            if (TouchPanel.EnabledGestures != GestureType.None)
            {
                CurrentGame.Gestures.Clear();
                while (TouchPanel.IsGestureAvailable)
                    CurrentGame.Gestures.Add(TouchPanel.ReadGesture());
            }

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

        #region Events

#if NETFX_CORE
        public abstract void OnSnap(bool isSmallSnap);
        private void checkSnapOnResize(object sender, EventArgs e)
        {
            CurrentGame.WindowSize = Window.ClientBounds.GetSize();

            if (!CurrentGame.WindowSize.X.Equals(CurrentGame.ScreenSize.X))
            {
                OnSnap((int) CurrentGame.WindowSize.X < CurrentGame.ScreenSize.X/2);
            }
        }
#endif

        #endregion

    }
}
