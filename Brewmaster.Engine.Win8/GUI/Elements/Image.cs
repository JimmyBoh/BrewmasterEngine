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
            TextureName = textureName;
            Scale = scale;
        }

        public string TextureName { get; set; }
        public float Scale { get; set; }
        
        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.DrawString(ContentHandler.Retrieve<SpriteFont>("DebugFont"), TextureName, RenderBounds.Location.ToVector2(), Color.Blue);
        }
    }
}
