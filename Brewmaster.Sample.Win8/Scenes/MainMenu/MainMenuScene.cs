using System;
using System.Collections.Generic;
using System.Linq;
using SampleGame.Scenes.MainMenu.Entities;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using SampleGame.Common;

namespace SampleGame.Scenes.MainMenu
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
