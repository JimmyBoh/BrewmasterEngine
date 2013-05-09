#region Using Statements
using System;
using System.Collections.Generic;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace BrewmasterEngine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TestGame : Game2D
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public TestGame() : base()
        {
            ScreenWidth = 1366;
            ScreenHeight = 768;

            BackgroundColor = Color.Black;
            DebugMode = false;
        }

        public override IEnumerable<string> PreloadTextures
        {
            get { return null; }
        }

        public override IEnumerable<string> PreloadFonts
        {
            get { return null; }
        }

        public override void Init()
        {
            
        }

        public override IEnumerable<Scene> Scenes
        {
            get { return null; }
        }
    }
}
