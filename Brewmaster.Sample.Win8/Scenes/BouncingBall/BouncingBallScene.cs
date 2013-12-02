using System;
using System.Linq;
using BrewmasterEngine.GUI;
using BrewmasterEngine.Graphics.Content;
using SampleGame.Scenes.BouncingBall.Entities;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;

namespace SampleGame.Scenes.BouncingBall
{
    public class BouncingBallScene : Scene
    {
        #region Constants



        #endregion

        #region Constructor

        public BouncingBallScene() : base(SceneNames.Sprites)
        {
            
        }

        #endregion

        #region Properties

        private BallManager ballMgr;
        private Layout ballGui;
        private Text totalCount;

        #endregion

        #region Methods

        protected override void Load()
        {
            Add(ballMgr = new BallManager());

            totalCount = new Text("DebugFont", "Total:0000");

            ballGui = new Layout();

            ballGui.CreateHorizontalLayout(
                root => root.AddVerticalPanel(2, 1, 1, col =>
                                                       col.AddVerticalPanel(1, 1, 6, center =>
                                                                                     center.AddChild(totalCount)))
                            .AddVerticalPanel(1, right =>
                                                 right.AddVerticalPanel(0, 1, 1, menu =>
                                                                                 menu.AddChild(new GameGuiButton("Pause", (btn) =>
                                                                                 {
                                                                                     CurrentGame.TogglePauseCurrentScene();
                                                                                     btn.Text = new StaticText(btn.Text.FontName, CurrentGame.IsPaused ? "Unpause" : "Pause");
                                                                                 }))
                                                                                    .AddChild(new AlterButton(-1000))
                                                                                    .AddChild(new AlterButton(-100))
                                                                                    .AddChild(new AlterButton(100))
                                                                                    .AddChild(new AlterButton(1000)))
                                                     ));

            Add(ballGui);
        }

        public void PauseScene()
        {
            CurrentGame.SceneManager.PauseCurrentScene();
        }

        public void UnpauseScene()
        {
            CurrentGame.SceneManager.UnpauseCurrentScene();
        }

        public override void Update(GameTime gameTime)
        {
            ForEachEntity(o => o.Update(gameTime));

            UpdateTotalCount(ballMgr.TotalBalls);
        }

        public void UpdateTotalCount(int count)
        {
            totalCount.DisplayText = string.Format("Total:{0:n0}", count);
            ballGui.Reflow();
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
