using System;
using Brewmaster.Engine.Win8.GUI;
using BrewmasterEngine.GUI;

namespace Brewmaster.Sample.Win8.Scenes.BouncingBall.Entities
{
    public class BallGameGui : Layout
    {
        public BallGameGui()
        {
            CreateHorizontalLayout(
                root => root.AddPanel(7, 1, LayoutStyle.Vertical,
                    col => col.AddPanel(0, 1, 1, LayoutStyle.Vertical,
                        menu => menu.AddChild(new Text("DebugFont", "Pause"))
                                    .AddChild(new Text("DebugFont", "-1000"))
                                    .AddChild(new Text("DebugFont", " -100"))
                                    .AddChild(new Text("DebugFont", " +100"))
                                    .AddChild(new Text("DebugFont", "+1000")))
                                .AddPanel(1)));
        }
    }
}
