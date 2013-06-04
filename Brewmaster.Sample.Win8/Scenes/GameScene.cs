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
        #region Constants

        private const string MENU_TAG = "menu";

        #endregion

        #region Constructor

        public GameScene() : base("game")
        {

        }

        #endregion

        #region Properties

        private BallManager ballMgr;

        #endregion


        #region Methods

        protected override void Load(Action done)
        {
            ballMgr = new BallManager();

            this.Add(ballMgr);

            this.Add(new MenuButton("Pause", new Vector2(CurrentGame.Window.ClientBounds.Width - 70, 50),
                                    (button, releasedOn) =>
                                        {
                                            button.Scale = Vector2.One;

                                            if (!releasedOn) return;

                                            PauseScene();

                                        }, onButtonDown));
            this.Add(new MenuButton("- 10", new Vector2(CurrentGame.Window.ClientBounds.Width - 70, 100),
                                    (button, releasedOn) =>
                                        {
                                            button.Scale = Vector2.One;

                                            if (releasedOn)
                                                RemoveBalls(10);

                                        }, onButtonDown));
            this.Add(new MenuButton("+ 10", new Vector2(CurrentGame.Window.ClientBounds.Width - 70, 150),
                                    (button, releasedOn) =>
                                        {
                                            button.Scale = Vector2.One;

                                            if (releasedOn)
                                                AddBall(10);
                                        }, onButtonDown));
            this.Add(new MenuButton("Resume", new Vector2(CurrentGame.Window.ClientBounds.Width/2.0f, 400),
                                    (button, releasedOn) =>
                                        {
                                            button.Scale = Vector2.One;

                                            if (releasedOn)
                                                UnpauseScene();
                                        }, onButtonDown) {AddTags = new[] {MENU_TAG}});
            this.Add(new MenuButton("Clear", new Vector2(CurrentGame.Window.ClientBounds.Width/2.0f, 500),
                                    (button, releasedOn) =>
                                        {
                                            button.Scale = Vector2.One;

                                            if (releasedOn)
                                                RemoveAllBalls();
                                        }, onButtonDown) {AddTags = new[] {MENU_TAG}});
            this.Add(new MenuButton("Quit", new Vector2(CurrentGame.Window.ClientBounds.Width/2.0f, 600),
                                    (button, releasedOn) =>
                                        {
                                            button.Scale = Vector2.One;

                                            if (releasedOn)
                                                CurrentGame.SceneManager.Load("main");
                                        }, onButtonDown) {AddTags = new[] {MENU_TAG}});

            this.ForEachEntity(o => o.Tags.Contains(MENU_TAG), o => o.IsVisible = false);

            done();
        }

        private void onButtonDown(MenuButton button)
        {
            button.Scale = new Vector2(0.9f);
        }

        private void PauseScene()
        {
            CurrentGame.SceneManager.PauseCurrentScene();
            this.ForEachEntity(o => o.Tags.Contains(MENU_TAG), o => o.Show());
        }

        private void UnpauseScene()
        {
            CurrentGame.SceneManager.UnpauseCurrentScene();
            this.ForEachEntity(o => o.Tags.Contains(MENU_TAG), o => o.Hide());
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        private void AddBall(int count = 1)
        {
            ballMgr.AddBalls(count);
        }

        private void RemoveBalls(int count = 1)
        {
            ballMgr.RemoveBalls(count);
        }

        private void RemoveAllBalls()
        {
            ballMgr.Clear();
        }

        #endregion

    }
}
