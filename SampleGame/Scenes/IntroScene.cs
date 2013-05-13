using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Debug;
using BrewmasterEngine.Extensions;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using SampleGame.GUI;

namespace SampleGame.Scenes
{
    public class IntroScene : Scene
    {
        public IntroScene() : base("intro")
        {

        }

        protected override void Load(Action done)
        {
            var windowBounds = CurrentGame.Window.ClientBounds;
            var windowSize = new Vector2(windowBounds.Width, windowBounds.Height);

            this.Add(new MenuText("Loading...", windowSize * new Vector2(0.5f, 0.2f)));
            this.Add(new MenuButton("Press Start!", windowSize * new Vector2(0.5f, 0.8f), "main"));

            done();
        }
    }
}
