using System;
using BrewmasterEngine.GUI;
using BrewmasterEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.GUI
{
    public class Text : Element
    {
        public Text(string fontName, string text) : this(fontName, text, Vector2.One) { }
        public Text(string fontName, string text, Vector2 scale)
        {
            DisplayText = text;
            FontName = ContentHandler.Load<SpriteFont>(fontName);
            
            textSize = Font.MeasureString(DisplayText);
            Bounds = new Rectangle(0, 0, (int)textSize.X, (int)textSize.Y);
            
            Scale = scale;
            ReflowScale = 1f;
        }

        public string DisplayText { get; set; }
        public string FontName { get; set; }
        public SpriteFont Font { get { return ContentHandler.Retrieve<SpriteFont>(FontName); } }
        private Vector2 textSize;

        public override void Reflow(Rectangle area)
        {
            ReflowScale = Math.Min(Parent.RenderBounds.Width / textSize.X, Parent.RenderBounds.Height / textSize.Y);

            Bounds = new Rectangle(area.X, area.Y, (int)(textSize.X * ReflowScale * Scale.X), (int)(textSize.Y * ReflowScale * Scale.Y));
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.DrawString(Font, DisplayText, RenderBounds.Location.ToVector2(), Color.White, 0f, Vector2.Zero, ReflowScale, SpriteEffects.None, 0);
        }
    }
}
