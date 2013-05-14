using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using SampleGame.GUI;
using SampleGame.Scenes;

namespace SampleGame
{
    public class SampleGame : Game2D
    {
        public SampleGame() : base()
        {
            ScreenWidth = 1366;
            ScreenHeight = 768;

            BackgroundColor = Color.Orange;
            DebugMode = true;
        }

        public override IEnumerable<string> PreloadTextures
        {
            get { return new string[]{}; }
        }

        public override IEnumerable<string> PreloadFonts
        {
            get { return new [] {"DebugFont"}; }
        }

        public override IEnumerable<GameObject> BackgroundObjects
        {
            get
            {
                return new GameObject[]
                    {
                        new GradientBackground(Color.Orange, Color.Blue, 800, 100.0f)
                    };
            }
        }

        public override IEnumerable<GameObject> ForegroundObjects
        {
            get
            {
                return new GameObject[]
                    {

                    };
            }
        }

        public override void Init()
        {
            
        }

        public override IEnumerable<Scene> Scenes
        {
            get
            {
                return new Scene[]
                    {
                        new IntroScene(),
                        new MainMenuScene()
                    };
            }
        }
    }
}
