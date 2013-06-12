using System;
using BrewmasterEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.GUI.Elements
{
    public class Header : Element
    {
        public Header(string fontName, string text, float scale = 1f)
        {
            Text = text;
            FontName = ContentHandler.Load<SpriteFont>(fontName);
            
            var size = Font.MeasureString(Text);
            Bounds = TextSize = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            
            Scale = scale;
            reflowScale = 1f;
        }

        public string Text { get; set; }
        public string FontName { get; set; }
        public SpriteFont Font { get { return ContentHandler.Retrieve<SpriteFont>(FontName); } }
        private Rectangle TextSize;
        public float Scale { get; set; }

        public override void Reflow()
        {
            reflowScale = Math.Min((float)Parent.RenderBounds.Width / TextSize.Width, (float)Parent.RenderBounds.Height / TextSize.Height);

            Bounds = new Rectangle((Parent.Bounds.Width - (int)(TextSize.Width * reflowScale * Scale)) / 2, (Parent.Bounds.Height - (int)(TextSize.Height * reflowScale * Scale)) / 2, (int)(TextSize.Width * reflowScale * Scale), (int)(TextSize.Height * reflowScale * Scale));
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.DrawString(Font, Text, RenderBounds.Location.ToVector2(), Color.White, 0f, Vector2.Zero, reflowScale, SpriteEffects.None, 0);
        }
    }
}
