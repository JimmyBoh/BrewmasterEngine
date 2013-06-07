using System;
using System.Collections.Generic;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brewmaster.Engine.Win8.Graphics.Content
{
    public class StaticImage : GameObject
    {
        public StaticImage(string imageName, Rectangle bounds, string objectName = "") : base(objectName)
        {
            Bounds = bounds;
            TextureName = ContentHandler.Load<Texture2D>(imageName);
        }

        public Rectangle Bounds { get; set; }
        public string TextureName { get; set; }

        public override void Update(GameTime gameTime)
        {
            // Do nothing...
        }

        public override void Draw(GameTime gameTime)
        {
            if (!IsVisible) return;

            spriteBatch.Draw(ContentHandler.Retrieve<Texture2D>(TextureName), Bounds, Color.White);
        }
    }
}
