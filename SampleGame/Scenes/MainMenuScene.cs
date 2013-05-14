using System;
using System.Collections.Generic;
using System.Linq;
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
            this.Add(new MenuButton("New Game", windowSize*new Vector2(0.5f, 0.7f), onButtonUp("intro"), onButtonDown()));

            this.Add(new MenuButton("Options", windowSize * new Vector2(0.5f, 0.8f), onButtonUp("intro"), onButtonDown()));
            this.Add(new MenuButton("Quit.", windowSize*new Vector2(0.5f, 0.9f), (button, releasedOn) =>
                {
                    if (releasedOn)
                        CurrentGame.Exit();
                    else
                        button.Scale = Vector2.One;
                }, onButtonDown()));

            done();
        }

        private Action<MenuButton> onButtonDown()
        {
            return (button) =>
                {
                    button.Scale = new Vector2(0.9f);
                };
        }

        private Action<MenuButton, bool> onButtonUp(string targetScene)
        {
            return (button, releasedOn) =>
                {
                    if (releasedOn)
                        CurrentGame.SceneManager.Load(targetScene);
                    else
                        button.Scale = Vector2.One;
                };
        }
    }
}
