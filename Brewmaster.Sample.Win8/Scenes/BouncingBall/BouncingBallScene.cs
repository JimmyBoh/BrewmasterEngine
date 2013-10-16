using System;
using System.Linq;
using SampleGame.Scenes.BouncingBall.Entities;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;

namespace SampleGame.Scenes.BouncingBall
{
    public class BouncingBallScene : Scene
    {
        #region Constants

        private const string MENU_TAG = "menu";

        #endregion

        #region Constructor

        public BouncingBallScene() : base(SceneNames.Sprites)
        {
            
        }

        #endregion

        #region Properties

        private BallManager ballMgr;
        private BallGameGui ballGui;

        #endregion

        #region Methods

        protected override void Load()
        {
            Add(ballMgr = new BallManager());
            Add(ballGui = new BallGameGui());
           
            //this.Add(new MenuButton("Quit", new Vector2(CurrentGame.Window.ClientBounds.Width/2.0f, 600),
            //                        (button, releasedOn) =>
            //                            {
            //                                button.Scale = Vector2.One;

            //                                if (releasedOn)
            //                                    CurrentGame.SceneManager.Load("main");
            //                            }, onButtonDown) {AddTags = new[] {MENU_TAG}});

            this.ForEachEntity(o => o.Tags.Contains(MENU_TAG), o => o.IsVisible = false);
        }

        public void PauseScene()
        {
            CurrentGame.SceneManager.PauseCurrentScene();
            this.ForEachEntity(o => o.Tags.Contains(MENU_TAG), o => o.Show());
        }

        public void UnpauseScene()
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

            ballGui.UpdateTotalCount(ballMgr.TotalBalls);
        }

        public void AddBall(int count = 1)
        {
            ballMgr.AdjustBallCount(count);
        }

        public void RemoveBall(int count = 1)
        {
            ballMgr.AdjustBallCount(-count);
        }

        public void RemoveAllBalls()
        {
            ballMgr.Clear();
        }

        #endregion

    }
}
