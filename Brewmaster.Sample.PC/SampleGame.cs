using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Debugging;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using SampleGame.Scenes;

namespace SampleGame
{
    public class BrewGame : Game2D
    {
        public BrewGame() : base()
        {
            BackgroundColor = Color.Black;
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

        public override GestureType EnabledGestures
        {
            get { return GestureType.Tap | GestureType.Pinch | GestureType.Flick | GestureType.DoubleTap | GestureType.HorizontalDrag | GestureType.FreeDrag; }
        }

        public override IEnumerable<GameObject> BackgroundObjects
        {
            get
            {
                return new GameObject[]
                    {
                        //new GradientBackground(Color.Orange, Color.Blue, 800, 1000.0f)
                    };
            }
        }

        public override IEnumerable<GameObject> ForegroundObjects
        {
            get
            {
                return new GameObject[]
                    {
                        new FpsCounter("DebugFont"),
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
                        new MainMenuScene(),
                        new GameScene()
                    };
            }
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
