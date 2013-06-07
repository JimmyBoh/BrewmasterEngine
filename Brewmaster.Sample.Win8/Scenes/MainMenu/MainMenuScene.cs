using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Framework;
using BrewmasterEngine.GUI;
using BrewmasterEngine.GUI.Elements;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using SampleGame.Menu.Widgets;

namespace SampleGame.Scenes.MainMenu
{
    public class MainMenuScene : Scene
    {
        public MainMenuScene() : base("main")
        {

        }

        protected override void Load()
        {
            GuiManager.CreateHorizontalLayout()
                      .AddPanel(1, Layout.Vertical,
                                colA => colA.AddPanel(3, 1, Layout.Layered,
                                                      settingsButton => settingsButton.AddChild(new Image("test"))))
                      .AddPanel(2, Layout.Vertical,
                                colB =>
                                    {
                                        colB.AddPanel(1, Layout.Layered,
                                                      title => title.AddChild(new Image("title")));
                                        colB.AddPanel(1, Layout.Layered,
                                                      title => title.AddChild(new Image("play")));
                                        colB.AddPanel(1, Layout.Layered,
                                                      title => title.AddChild(new Image("store")));
                                    })
                      .AddPanel(1, Layout.Vertical,
                                colC =>
                                    {
                                        colC.AddPanel(1, Layout.Layered,
                                                      muteButton => muteButton.AddChild(new Image("mute")));
                                        colC.AddPanel(2, 1, Layout.Horizontal, 
                                            bottomRight =>
                                            {
                                                bottomRight.AddPanel(1, Layout.Layered,
                                                                     comp => comp.AddChild(new Image("rovio")));
                                                bottomRight.AddPanel(1, Layout.Layered,
                                                                     credits => credits.AddChild(new Image("credits")));
                                            });
                                    });

            this.Add(new GradientBackground("OrangeBlueVertical", Color.Orange, Color.Blue, 500.0f));
            this.Add(new MenuText("Main Menu", CurrentGame.WindowSize * new Vector2(0.5f, 0.2f)));
            this.Add(new MenuButton("Start Game", CurrentGame.WindowSize * new Vector2(0.5f, 0.8f), LoadSceneOnButtonUp("game"), onButtonDown));
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
