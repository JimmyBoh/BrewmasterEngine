using System;
using BrewmasterEngine.Framework;
using BrewmasterEngine.GUI;
using BrewmasterEngine.Graphics.Content;

namespace SampleGame.Scenes.BouncingBall.Entities
{
    public class BallGameGui : Layout
    {
        private readonly Text totalCount;
        private readonly Layout ballGameLayout;

        public BallGameGui()
        {
            totalCount = new Text("DebugFont", "Total:0000");

            ballGameLayout = CreateHorizontalLayout(
                root => root.AddVerticalPanel(2, 1, 1, col =>
                                                       col.AddVerticalPanel(1, 1, 6, center =>
                                                                                     center.AddChild(totalCount)))
                            .AddVerticalPanel(1, right =>
                                                 right.AddVerticalPanel(0, 1, 1, menu =>
                                                                                 menu.AddChild(new GameGuiButton("Pause", (btn) =>
                                                                                     {
                                                                                         CurrentGame.PauseCurrentScene();
                                                                                         btn.Text = new StaticText(btn.Text.FontName, CurrentGame.IsPaused ? "Unpause" : "Pause");
                                                                                     }))
                                                                                    .AddChild(new AlterButton(-1000))
                                                                                    .AddChild(new AlterButton(-100))
                                                                                    .AddChild(new AlterButton(100))
                                                                                    .AddChild(new AlterButton(1000)))
                                                     ));
        }

        public void UpdateTotalCount(int count)
        {
            totalCount.DisplayText = string.Format("Total:{0:n0}", count);
            ballGameLayout.Reflow();
        }
    }
}