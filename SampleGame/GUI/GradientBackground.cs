using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics.Content;
using BrewmasterEngine.Graphics.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SampleGame.GUI
{
    public class GradientBackground : GameObject
    {
        public GradientBackground(Color start, Color stop, bool horizontal = false)
        {
            IsVisible = true;

            startColor = start;
            stopColor = stop;
            isHorizontal = horizontal;

            UpdateBounds();
            CurrentGame.Window.ClientSizeChanged += UpdateBounds;
        }

        private void UpdateBounds(object sender = null, EventArgs args = null)
        {
            Bounds = CurrentGame.Window.ClientBounds;
            texture = isHorizontal
                ? Gradient.CreateHorizontal(Bounds.Width, startColor, stopColor) 
                : Gradient.CreateVertical(Bounds.Height, startColor, stopColor);
        }

        private Rectangle Bounds;
        private Texture2D texture;
        private readonly bool isHorizontal;
        private readonly Color startColor;
        private readonly Color stopColor;

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime elapsedTime)
        {
            spriteBatch.Draw(texture,Bounds,Color.White);

        }
    }
}
