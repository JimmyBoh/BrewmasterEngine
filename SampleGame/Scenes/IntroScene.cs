using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using SampleGame.Menu.Widgets;

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

            this.Add(new GradientBackground(Color.Blue, Color.Red, 1000, 200.0f, true));
            this.Add(new MenuText("Loading...", windowSize*new Vector2(0.5f, 0.2f)));
            this.Add(new MenuButton("Go to Main Menu", windowSize*new Vector2(0.5f, 0.8f), (button, releasedOn) =>
                {
                    if (releasedOn)
                        CurrentGame.SceneManager.Load("main");
                    else
                        button.Scale = Vector2.One;
                },
                    (button) =>
                        {
                            button.Scale = new Vector2(0.9f);
                        }));

            done();
        }
    }
}