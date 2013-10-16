using System;
using System.Collections.Generic;
using BrewmasterEngine.GUI;

namespace SampleGame.Scenes.MainMenu.Entities
{
    public class MainMenuLayout : Layout
    {
        public MainMenuLayout()
        {
            CreateHorizontalLayout(root =>
                root.AddVerticalPanel(1, 2, 1,
                    colB => 
                        colB.AddVerticalPanel(1,
                            title => title.AddChild(new Text("DebugFont", "Brewmaster\n  Samples "))
                        )
                        .AddVerticalPanel(2,
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
