using System;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics;
using BrewmasterEngine.Graphics.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SampleGame.Menu.Widgets
{
    public class GradientBackground : GameObject
    {
        public GradientBackground(string name, Color start, Color stop, float scrollspeed = 1.0f, bool horizontal = false)
        {
            startColor = start;
            stopColor = stop;
            isHorizontal = horizontal;
            scrollSpeed = scrollspeed;
            scrollDir = 1;
            Name = name;

            position = Vector2.Zero;

            UpdateBounds();
            CurrentGame.Window.ClientSizeChanged += UpdateBounds;
        }

        private void UpdateBounds(object sender = null, EventArgs args = null)
        {
            Bounds = isHorizontal 
                ? new Rectangle((int)position.X, (int)position.Y, CurrentGame.MaxTextureSize, CurrentGame.Window.ClientBounds.Height) 
                : new Rectangle((int)position.X, (int)position.Y, CurrentGame.Window.ClientBounds.Width, CurrentGame.MaxTextureSize);


            textureName = ContentHandler.Load(Name, isHorizontal
                            ? Gradient.CreateHorizontal(Bounds.Width, startColor, stopColor)
                            : Gradient.CreateVertical(Bounds.Height, startColor, stopColor));
        }

        private Rectangle Bounds;
        private string textureName;
        private Vector2 position;
        private readonly bool isHorizontal;
        private readonly Color startColor;
        private readonly Color stopColor;
        private readonly float scrollSpeed;
        public int scrollDir;

        public override void Update(GameTime gameTime)
        {
            if (isHorizontal)
            {
                if (position.X >= 0)
                    scrollDir = -1;
                else if (Bounds.Right <= CurrentGame.Window.ClientBounds.Width)
                    scrollDir = 1;

                position += new Vector2((float)(scrollSpeed * gameTime.ElapsedGameTime.TotalSeconds * scrollDir), 0.0f);
            }
            else
            {
                if (position.Y >= 0)
                    scrollDir = -1;
                else if (Bounds.Bottom <= CurrentGame.Window.ClientBounds.Height)
                    scrollDir = 1;

                position += new Vector2(0.0f, (float)(scrollSpeed * gameTime.ElapsedGameTime.TotalSeconds * scrollDir));
            }
            var clampedX = MathHelper.Clamp(position.X, CurrentGame.Window.ClientBounds.Width - Bounds.Width, 0.0f);
            var clampedY = MathHelper.Clamp(position.Y, CurrentGame.Window.ClientBounds.Height - Bounds.Height, 0.0f);
            
            position = new Vector2(clampedX, clampedY);
            
            Bounds.Location = position.ToPoint();
        }

        public override void Draw(GameTime elapsedTime)
        {
            spriteBatch.Draw(ContentHandler.Retrieve<Texture2D>(textureName), Bounds, Color.White);
        }
    }
}
