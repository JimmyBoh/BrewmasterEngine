using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using SampleGame.Menu.Widgets;

namespace SampleGame.Scenes
{
    public class MainMenuScene : Scene
    {
        public MainMenuScene() : base("main")
        {

        }

        protected override void Load(Action done)
        {
            this.Add(new GradientBackground("OrangeBlueVertical", Color.Orange, Color.Blue, 500.0f));
            this.Add(new MenuText("Main Menu", CurrentGame.WindowSize * new Vector2(0.5f, 0.2f)));
            this.Add(new MenuButton("Start Game", CurrentGame.WindowSize * new Vector2(0.5f, 0.8f), LoadSceneOnButtonUp("game"), onButtonDown));

            done();
        }

        private void onButtonDown(MenuButton button)
        {
            button.Scale = new Vector2(0.9f);
        }

        private Action<MenuButton, bool> LoadSceneOnButtonUp(string targetScene)
        {
            return (button, releasedOn) =>
                {
                    button.Scale = Vector2.One;

                    if (releasedOn)
                        CurrentGame.SceneManager.Load(targetScene); 
                };
        }
    }
}
