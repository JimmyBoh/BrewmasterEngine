using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using SampleGame.GUI;

namespace SampleGame.Scenes
{
    public class MainMenuScene : Scene
    {
        public MainMenuScene() : base("main")
        {
        }

        protected override void Load(Action done)
        {
            var windowBounds = CurrentGame.Window.ClientBounds;
            var windowSize = new Vector2(windowBounds.Width, windowBounds.Height);

            this.Add(new MenuText("Main Menu", windowSize * new Vector2(0.5f, 0.2f)));
            this.Add(new MenuButton("Quit.", windowSize * new Vector2(0.5f, 0.8f), "intro"));

            done();
        }
    }
}
