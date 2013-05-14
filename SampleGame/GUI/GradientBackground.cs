using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SampleGame.GUI
{
    public class GradientBackground : GameObject
    {
        public GradientBackground(Color start, Color stop, int scrollamount = 0, float scrollspeed = 1.0f, bool horizontal = false)
        {
            IsVisible = true;

            startColor = start;
            stopColor = stop;
            isHorizontal = horizontal;
            scrollAmount = scrollamount;
            scrollSpeed = scrollspeed;
            scrollDir = 1;

            position = Vector2.Zero;

            UpdateBounds();
            CurrentGame.Window.ClientSizeChanged += UpdateBounds;
        }

        private void UpdateBounds(object sender = null, EventArgs args = null)
        {
            Bounds = new Rectangle((int)position.X, (int)position.Y, CurrentGame.Window.ClientBounds.Width, CurrentGame.Window.ClientBounds.Height);

            if (isHorizontal)
                Bounds.Inflate(scrollAmount, 0);
            else
                Bounds.Inflate(0, scrollAmount);


            texture = isHorizontal
                ? Gradient.CreateHorizontal(Bounds.Width + scrollAmount, startColor, stopColor) 
                : Gradient.CreateVertical(Bounds.Height + scrollAmount, startColor, stopColor);
        }

        private Rectangle Bounds;
        private Texture2D texture;
        private Vector2 position;
        private readonly bool isHorizontal;
        private readonly Color startColor;
        private readonly Color stopColor;
        private readonly int scrollAmount;
        private readonly float scrollSpeed;
        public int scrollDir;

        public override void Update(GameTime gameTime)
        {
            if (scrollAmount == 0) return;

            if (isHorizontal)
            {
                if (position.X >= 0)
                    scrollDir = -1;
                else if (Bounds.Right <= CurrentGame.Window.ClientBounds.Width)
                    scrollDir = 1;

                position += new Vector2((float)(scrollSpeed * gameTime.ElapsedGameTime.TotalSeconds * scrollDir), 0.0f);

                Console.WriteLine("{0}", position.X);
            }
            else
            {
                if (position.Y >= 0)
                    scrollDir = -1;
                else if (Bounds.Bottom <= CurrentGame.Window.ClientBounds.Height)
                    scrollDir = 1;

                position += new Vector2(0.0f, (float)(scrollSpeed * gameTime.ElapsedGameTime.TotalSeconds * scrollDir));

                Console.WriteLine("{0}", position.Y);
            }

            Bounds.Location = new Point((int) position.X, (int) position.Y);
        }

        public override void Draw(GameTime elapsedTime)
        {
            spriteBatch.Draw(texture, Bounds, Color.White);

        }
    }
}
