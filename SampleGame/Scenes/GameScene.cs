using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using SampleGame.Entities;
using SampleGame.Menu.Widgets;

namespace SampleGame.Scenes
{
    public class GameScene : Scene
    {
        public GameScene() : base("game") { }

        private int ballCount;

        protected override void Load(Action done)
        {
            AddNewBall();

            this.Add(new MenuButton("Pause", new Vector2(CurrentGame.Window.ClientBounds.Width - 70, 50), (button, releasedOn) =>
                {
                    if (releasedOn)
                    {
                        CurrentGame.SceneManager.TogglePauseCurrentScene();
                    }
                }));
            
            this.Add(new MenuButton("+ 1", new Vector2(CurrentGame.Window.ClientBounds.Width - 70, 100), (button, releasedOn) =>
                {
                    if (releasedOn)
                    {
                        AddNewBall();
                    }
                }));
            this.Add(new MenuButton("+ 10", new Vector2(CurrentGame.Window.ClientBounds.Width - 70, 150), (button, releasedOn) =>
            {
                if (releasedOn)
                {
                    AddNewBall(10);
                }
            }));
        }

        private void AddNewBall(int count = 1)
        {
            for (var i = 0; i < count;i++)
                this.Add(new Ball(CurrentGame.Random.Next(32, 48), CurrentGame.Random.Next(200, 800)));

            ballCount += count;
            Console.WriteLine(ballCount);
        }
    }
}
