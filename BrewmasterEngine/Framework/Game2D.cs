using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;

namespace BrewmasterEngine.Framework
{
    public abstract class Game2D : IDisposable
    {
        #region Constructor

        protected Game2D()
        {
            ScreenWidth = 800;
            ScreenHeight = 480;
            ContentRoot = "Content";
            DefaultFontPath = string.Empty;
            Fullscreen = false;
            DebugMode = false;
        }

        #endregion

        #region Properties

        public string ContentRoot { get; set; }
        public string DefaultFontPath { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public bool Fullscreen { get; set; }
        public Color BackgroundColor { get; set; }
        public bool DebugMode { get; set; }

        public abstract IEnumerable<string> PreloadTextures { get; }
        public abstract IEnumerable<string> PreloadFonts { get; }

        private GameEngine gameEngine;

        #endregion

        #region Abstract Methods

        public abstract void Init();
        public abstract IEnumerable<Scene> Scenes { get; }

        #endregion

        #region Methods

        public void Run()
        {
            using (gameEngine = new GameEngine(this))
            {
                gameEngine.Run();
            }
        }

        public void Dispose()
        {
            if (gameEngine != null)
                gameEngine.Dispose();
        }

        #endregion

        
    }
}
