using System;
using System.Collections.Generic;
using BrewmasterEngine.GUI;
using BrewmasterEngine.GUI.Elements;

namespace Brewmaster.Sample.Win8.Scenes.MainMenu.Entities
{
    public class MainMenuLayout : Layout
    {
        public MainMenuLayout()
        {
            CreateHorizontalLayout(root =>
                root.AddPanel(1, 2, LayoutStyle.Vertical,
                    colB => 
                        colB.AddPanel(1, LayoutStyle.Layered,
                            title => title.AddChild(new Header("DebugFont", "Brewmaster\n  Samples ")))
                        .AddPanel(2, LayoutStyle.Horizontal, 
                            scenes => scenes.AddPanel(1)
                        .AddPanel(1, LayoutStyle.Vertical, 
                            demos => 
                                demos.AddPanel(1, LayoutStyle.Layered, 
                                    sprites => sprites.AddChild(new Header("DebugFont", "Sprites")))
                                .AddPanel(1, LayoutStyle.Layered, 
                                    touch => touch.AddChild(new Header("DebugFont", "Touch"))))
                        .AddPanel(1)))
            .AddPanel(1));
        }
    }
}
