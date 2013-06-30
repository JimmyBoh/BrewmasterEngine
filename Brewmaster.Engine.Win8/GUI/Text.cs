using System;
using BrewmasterEngine.GUI;
using BrewmasterEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brewmaster.Engine.Win8.GUI
{
    public class Text : Element
    {
        public Text(string fontName, string text, float scale = 1f)
        {
            DisplayText = text;
            FontName = ContentHandler.Load<SpriteFont>(fontName);
            
            textSize = Font.MeasureString(DisplayText);
            Bounds = new Rectangle(0, 0, (int)textSize.X, (int)textSize.Y);
            
            Scale = scale;
            reflowScale = 1f;
        }

        public string DisplayText { get; set; }
        public string FontName { get; set; }
        public SpriteFont Font { get { return ContentHandler.Retrieve<SpriteFont>(FontName); } }
        private Vector2 textSize;
        public float Scale { get; set; }

        public override void Reflow()
        {
            reflowScale = Math.Min(Parent.RenderBounds.Width / textSize.X, Parent.RenderBounds.Height / textSize.Y);

            Bounds = new Rectangle(Parent.Bounds.X, Parent.Bounds.Y, (int)(textSize.X * reflowScale * Scale), (int)(textSize.Y * reflowScale * Scale));

            base.Reflow();
        }

        public override void Draw(GameTime gameTime)
        {
            //var rect = new Rectangle(RenderBounds.Location.X, RenderBounds.Location.Y, (int)(RenderBounds.Width * reflowScale), (int)(RenderBounds.Height * reflowScale));
            //spriteBatch.FillRectangle(Parent.RenderBounds, Color.Red*0.5f);
            //spriteBatch.FillRectangle(rect,Color.Blue*0.5f);
            spriteBatch.DrawString(Font, DisplayText, RenderBounds.Location.ToVector2(), Color.White, 0f, Vector2.Zero, reflowScale, SpriteEffects.None, 0);
        }
    }
}
