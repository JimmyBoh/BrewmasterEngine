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

        private const string menuTag = "menu";

        protected override void Load(Action done)
        {
            AddNewBall();

            this.Add(new MenuButton("Pause", new Vector2(CurrentGame.Window.ClientBounds.Width - 70, 50), (button, releasedOn) =>
                {
                    button.Scale = Vector2.One;

                    if (!releasedOn) return;

                    PauseScene();

                }, onButtonDown));
            this.Add(new MenuButton("+ 1", new Vector2(CurrentGame.Window.ClientBounds.Width - 70, 100), (button, releasedOn) =>
                {
                    button.Scale = Vector2.One;

                    if (releasedOn)
                        AddNewBall();

                }, onButtonDown));
            this.Add(new MenuButton("+ 10", new Vector2(CurrentGame.Window.ClientBounds.Width - 70, 150), (button, releasedOn) =>
                {
                    button.Scale = Vector2.One;

                    if (releasedOn)
                        AddNewBall(10);
                }, onButtonDown));
            this.Add(new MenuButton("Resume", new Vector2(CurrentGame.Window.ClientBounds.Width/2.0f, 400), (button, releasedOn) =>
                {
                    button.Scale = Vector2.One;

                    if (releasedOn)
                        UnpauseScene();
                }, onButtonDown) { AddTags = new[] { menuTag } });
            this.Add(new MenuButton("Clear", new Vector2(CurrentGame.Window.ClientBounds.Width / 2.0f, 500), (button, releasedOn) =>
            {
                button.Scale = Vector2.One;

                if (releasedOn)
                    RemoveAllBalls();
            }, onButtonDown) { AddTags = new[] { menuTag } });
            this.Add(new MenuButton("Quit", new Vector2(CurrentGame.Window.ClientBounds.Width / 2.0f, 600), (button, releasedOn) =>
            {
                button.Scale = Vector2.One;

                if (releasedOn)
                    CurrentGame.SceneManager.Load("main");
            }, onButtonDown) { AddTags = new[] { menuTag } });

            this.ForEachEntity(o => o.Tags.Contains(menuTag), o => o.IsVisible = false);

            done();
        }

        private void onButtonDown(MenuButton button)
        {
            button.Scale = new Vector2(0.9f);
        }

        private void PauseScene()
        {
            CurrentGame.SceneManager.PauseCurrentScene();
            this.ForEachEntity(o => o.Tags.Contains(menuTag), o => o.Show());
        }
        
        private void UnpauseScene()
        {
            CurrentGame.SceneManager.UnpauseCurrentScene();
            this.ForEachEntity(o => o.Tags.Contains(menuTag), o => o.Hide());
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        private void AddNewBall(int count = 1)
        {
            for (var i = 0; i < count;i++)
                this.Add(new Ball());
        }

        private void RemoveAllBalls()
        {
            this.Remove(this.Entities.Where(e => e is Ball));
        }
    }
}
