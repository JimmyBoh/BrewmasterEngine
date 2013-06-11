using System;
using BrewmasterEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.GUI.Elements
{
    public class Image : Element
    {
        public Image(string textureName, float scale = 1f)
        {
            TextureName = ContentHandler.Load<Texture2D>(textureName);
            Scale = scale;
            reflowScale = 1f;
        }

        public string TextureName { get; set; }
        public Texture2D Texture { get { return ContentHandler.Retrieve<Texture2D>(TextureName); } }
        public float Scale { get; set; }

        public override void Reflow()
        {
            reflowScale = Math.Min((float)Parent.RenderBounds.Width / Texture.Width, (float)Parent.RenderBounds.Height / Texture.Height);

            Bounds = new Rectangle((Parent.Bounds.Width - (int)(Texture.Width * reflowScale * Scale)) / 2, (Parent.Bounds.Height - (int)(Texture.Height * reflowScale * Scale)) / 2, (int)(Texture.Width * reflowScale * Scale), (int)(Texture.Height * reflowScale * Scale));
        }
        
        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(ContentHandler.Retrieve<Texture2D>(TextureName), RenderBounds, Color.White);
        }
    }
}
