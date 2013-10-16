using System;
using BrewmasterEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.GUI
{
    public class Image : Element
    {
        public Image(string textureName):this(textureName, Vector2.One) { }
        public Image(string textureName, Vector2 scale)
        {
            TextureName = ContentHandler.Load<Texture2D>(textureName);
            Scale = scale;
            ReflowScale = 1f;
        }

        public string TextureName { get; set; }
        public Texture2D Texture { get { return ContentHandler.Retrieve<Texture2D>(TextureName); } }

        public override void Reflow(Rectangle area)
        {
            ReflowScale = Math.Min((float)Parent.RenderBounds.Width / Texture.Width, (float)Parent.RenderBounds.Height / Texture.Height);

            Bounds = new Rectangle((area.Width - (int)(Texture.Width * ReflowScale * Scale.X)) / 2, (area.Height - (int)(Texture.Height * ReflowScale * Scale.Y)) / 2, (int)(Texture.Width * ReflowScale * Scale.X), (int)(Texture.Height * ReflowScale * Scale.Y));
        }
        
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(ContentHandler.Retrieve<Texture2D>(TextureName), RenderBounds, Color.White);
        }
    }
}
