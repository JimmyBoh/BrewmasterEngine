﻿using BrewmasterEngine.Extensions;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics.Core;
using Microsoft.Xna.Framework;

namespace SampleGame.GUI
{
    public class MenuText : SpriteText
    {
        public MenuText(string text, Vector2 position) : base("DebugFont", text)
        {
            Position = position;
        }
    }
}
