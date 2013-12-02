using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Debugging;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Brewmaster.Sample.Win8.Scenes.BouncingBall;
using Brewmaster.Sample.Win8.Scenes.MainMenu;

namespace Brewmaster.Sample.Win8
{
    public class SampleExplorer : Game2D
    {
        public SampleExplorer() : base()
        {
            BackgroundColor = Color.Black;
            DebugMode = true;
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


        public override IEnumerable<Scene> Scenes
        {
            get
            {
                return new Scene[]
                    {
                        new MainMenuScene(),
                        new BouncingBallScene()
                    };
            }
        }

        public override void PreloadContent()
        {
            ContentHandler.Preload<SpriteFont>(new[] {"DebugFont"});
        }

#if NETFX_CORE
public override void OnSnap(bool isSmallSnap)
        {
            if (isSmallSnap)
            {
                //CurrentGame.SceneManager.PauseCurrentScene();
            }
        }
#endif


        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
