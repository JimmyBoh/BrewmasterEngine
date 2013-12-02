using System;
using System.Collections.Generic;
using System.Linq;
using Brewmaster.Sample.Win8.Scenes.MainMenu.Entities;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using Brewmaster.Sample.Win8;
using Brewmaster.Sample.Win8.Common;

namespace Brewmaster.Sample.Win8.Scenes.MainMenu
{
    public class MainMenuScene : Scene
    {
        public MainMenuScene() : base(SceneNames.MainMenu)
        {

        }

        protected override void Load()
        {
            Add(new GradientBackground("OrangeBlueVertical", Color.Orange, Color.Blue, 500.0f));
            Add(new MainMenuLayout());
        }
    }
}
