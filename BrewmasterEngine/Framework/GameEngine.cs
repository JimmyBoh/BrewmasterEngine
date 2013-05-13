using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.Framework
{
    public class GameEngine : Game
    {
        public GameEngine(Game2D game)
        {
            this.game = game;
            CurrentGame.SetGame(this);

            Graphics = new GraphicsDeviceManager(this);
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Content.RootDirectory = game.ContentRoot;

            if (game.ScreenWidth > 0)
                Graphics.PreferredBackBufferWidth = game.ScreenWidth;
            if (game.ScreenHeight > 0)
                Graphics.PreferredBackBufferHeight = game.ScreenHeight;

            this.IsFixedTimeStep = false;
            Graphics.SynchronizeWithVerticalRetrace = false;
            Graphics.ApplyChanges();

            SceneManager = new SceneManager();
            globalObjects = new Dictionary<string, GameObject>();
        }

        #region Properties

        public SpriteBatch SpriteBatch { get; set; }
        public GraphicsDeviceManager Graphics { get; private set; }
        public Color BackgroundColor { get; set; }

        public SceneManager SceneManager { get; set; }

        private readonly Dictionary<string, GameObject> globalObjects;
        private readonly Game2D game;

        #endregion

        #region Methods

        protected override void Initialize()
        {
            game.Init();

            SceneManager.AddScenes(game.Scenes);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            if(game.PreloadTextures != null)
                Preload<Texture2D>(game.PreloadTextures.ToArray());
            if (game.PreloadFonts != null)
                Preload<SpriteFont>(game.PreloadFonts.ToArray());

            SceneManager.LoadDefaultScene();

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();

            SceneManager.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var keys = globalObjects.Keys;
            foreach (var k in keys)
                globalObjects[k].Update(gameTime);

            SceneManager.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.Clear(BackgroundColor);
            GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            SpriteBatch.Begin();
            SceneManager.Draw(gameTime);

            var keys = globalObjects.Keys;
            foreach (var k in keys)
                globalObjects[k].Draw(gameTime);

            SpriteBatch.End();
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
                    if (CurrentGame.DebugMode)
                        Console.WriteLine("! Failed to preload Texture2D[{0}]...", asset);
                }
            }
        }

        #endregion
        
    }
}
