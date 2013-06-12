using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.Graphics.Content
{
    public class StaticText : SpriteText
    {
        public StaticText(string fontName, string text, float scale = 1f) : base(fontName, text, "")
        {
            Scale = new Vector2(scale);
        }

        public void CenterOn(Vector2 point)
        {
            var halfSize = ContentHandler.Retrieve<SpriteFont>(FontName).MeasureString(Text) * Scale * 0.5f*0;
            Position = point - halfSize;
        }
    }
}
