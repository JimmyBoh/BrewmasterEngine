using System;
using System.Linq;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using SampleGame.Scenes.BouncingBall.Entities;

namespace SampleGame.Scenes.BouncingBall
{
    public class BouncingBallScene : Scene
    {
        #region Constants

        private const string MENU_TAG = "menu";
        private const int BALL_COUNT = 100;

        #endregion

        #region Constructor

        public BouncingBallScene() : base("ball")
        {
            ballsToAdd = 0;
        }

        #endregion

        #region Properties

        private BallManager ballMgr;
        private int ballsToAdd;

        #endregion

        #region Methods

        protected override void Load()
        {
            ballMgr = new BallManager();

            Add(ballMgr);

            //this.Add(new MenuButton("Pause", new Vector2(CurrentGame.Window.ClientBounds.Width - 70, 50),
            //                        (button, releasedOn) =>
            //                            {
            //                                button.Scale = Vector2.One;

            //                                if (!releasedOn) return;

            //                                PauseScene();

            //                            }, onButtonDown));
            //this.Add(new MenuButton("- " + BALL_COUNT, new Vector2(CurrentGame.Window.ClientBounds.Width - 70, 100),
            //                        (button, releasedOn) =>
            //                            {
            //                                button.Scale = Vector2.One;

            //                                if (releasedOn)
            //                                    RemoveBalls(BALL_COUNT);

            //                            }, onButtonDown));
            //this.Add(new MenuButton("+ "+BALL_COUNT, new Vector2(CurrentGame.Window.ClientBounds.Width - 70, 150),
            //                        (button, releasedOn) =>
            //                            {
            //                                button.Scale = Vector2.One;

            //                                if (releasedOn)
            //                                    ballsToAdd += BALL_COUNT;

            //                            }, onButtonDown));
            //this.Add(new MenuButton("Resume", new Vector2(CurrentGame.Window.ClientBounds.Width/2.0f, 400),
            //                        (button, releasedOn) =>
            //                            {
            //                                button.Scale = Vector2.One;

            //                                if (releasedOn)
            //                                    UnpauseScene();
            //                            }, onButtonDown) {AddTags = new[] {MENU_TAG}});
            //this.Add(new MenuButton("Clear", new Vector2(CurrentGame.Window.ClientBounds.Width/2.0f, 500),
            //                        (button, releasedOn) =>
            //                            {
            //                                button.Scale = Vector2.One;

            //                                if (releasedOn)
            //                                    RemoveAllBalls();
            //                            }, onButtonDown) {AddTags = new[] {MENU_TAG}});
            //this.Add(new MenuButton("Quit", new Vector2(CurrentGame.Window.ClientBounds.Width/2.0f, 600),
            //                        (button, releasedOn) =>
            //                            {
            //                                button.Scale = Vector2.One;

            //                                if (releasedOn)
            //                                    CurrentGame.SceneManager.Load("main");
            //                            }, onButtonDown) {AddTags = new[] {MENU_TAG}});

            this.ForEachEntity(o => o.Tags.Contains(MENU_TAG), o => o.IsVisible = false);
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
            if(CurrentGame.IsPaused)
                ForEachEntity(o => o.Tags.Contains(MENU_TAG), o => o.Update(gameTime));
            else
                ForEachEntity(o => !o.Tags.Contains(MENU_TAG), o => o.Update(gameTime));

            if (CurrentGame.Gestures.Any(g => g.GestureType == GestureType.DoubleTap))
                ballsToAdd += 100;

            if (ballsToAdd > 0)
            {
                AddBall(10);
            }
        }


        private void AddBall(int count = 1)
        {
            ballsToAdd -= count;
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
