using System;
using System.Collections.Generic;
using Brewmaster.Engine.Win8.GUI;
using BrewmasterEngine.GUI;

namespace Brewmaster.Sample.Win8.Scenes.MainMenu.Entities
{
    public class MainMenuLayout : Layout
    {
        public MainMenuLayout()
        {
            CreateHorizontalLayout(root =>
                root.AddPanel(1, 2, 1, LayoutStyle.Vertical,
                    colB => 
                        colB.AddPanel(1, LayoutStyle.Layered,
                            title => title.AddChild(new Text("DebugFont", "Brewmaster\n  Samples "))
                        )
                        .AddPanel(2, LayoutStyle.Vertical,
                            scenes => scenes.AddChild(new DemoButton("Sprites", SceneNames.Sprites))
                                            .AddChild(new DemoButton("Touch", SceneNames.Touch))
                                            .AddChild(new DemoButton("Animation", SceneNames.Animation))
                                            .AddChild(new DemoButton("Physics", SceneNames.Physics))
                        )
                    )
            );
        }
    }
}
