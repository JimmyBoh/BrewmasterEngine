using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Extensions;
using BrewmasterEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SampleGame.Entities
{
    public class Ball : GameObject
    {
        #region Constructor

        public Ball()
        {
            Radius = CurrentGame.Random.Next(4, 32);
            ZIndex = (int)radius - 32;
            maxSpeed = (float)Math.Pow(Radius - 3, 2);
            darkness = radius/32;

            Position = CurrentGame.Window.ClientBounds.GetRandomPoint();
            
            var dirX = CurrentGame.Random.Next(0, 2);
            if (dirX == 0)
                dirX = -1;

            var dirY = CurrentGame.Random.Next(-1, 2);
            if (dirY == 0)
                dirY = -1;

            Velocity = new Vector2(CurrentGame.Random.Next(100, 500) * dirX, CurrentGame.Random.Next(100, 500) * dirY);
        }

        #endregion

        #region Properties

        private float radius;

        public float Radius
        {
            get { return radius; }
            private set
            {
                radius = value;
                UpdateBounds();
            }
        }

        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            private set
            {
                position = value;
                UpdateBounds();
            }
        }

        public Rectangle Bounds { get; private set; }
        private void UpdateBounds()
        {
            Bounds = new Rectangle((int) (Position.X - radius), (int) (Position.Y - radius), (int) radius*2, (int) radius*2);
        }

        public Vector2 Velocity { get; private set; }
        private float acceleration = -1.1f;
        private readonly float maxSpeed;
        private readonly float darkness;

        #endregion

        #region Methods

        public override void Update(GameTime gameTime)
        {
            if (CurrentGame.IsPaused) return;

            var deltaX = Velocity.X;
            var deltaY = Velocity.Y;

            var posX = Position.X;
            var posY = Position.Y;

            if (Bounds.Left < 0 || Bounds.Right > CurrentGame.Window.ClientBounds.Width)
            {
                deltaX *= acceleration;
                deltaX = MathHelper.Clamp(deltaX, -maxSpeed, maxSpeed);
                Velocity = new Vector2(deltaX, deltaY);

                if (Bounds.Left < 0)
                    posX = radius;
                else
                    posX = CurrentGame.Window.ClientBounds.Width - Radius;
            }

            if (Bounds.Top < 0 || Bounds.Bottom > CurrentGame.Window.ClientBounds.Height)
            {
                deltaY *= acceleration;
                deltaY = MathHelper.Clamp(deltaY, -maxSpeed, maxSpeed);
                Velocity = new Vector2(deltaX, deltaY);
                
                if(Bounds.Top < 0)
                    posY = radius;
                else
                    posY = CurrentGame.Window.ClientBounds.Height - Radius;
            }

            if (Math.Abs(Velocity.X).Equals(maxSpeed) && Math.Abs(Velocity.Y).Equals(maxSpeed) && acceleration.Equals(-1.1f))
                acceleration = -0.9f;
            else if (Math.Abs(Velocity.X) < 200.0f && Math.Abs(Velocity.Y) < 200.0f && acceleration.Equals(-0.9f))
                acceleration = -1.1f;

            posX += (float)(deltaX * gameTime.ElapsedGameTime.TotalSeconds);
            posY += (float)(deltaY * gameTime.ElapsedGameTime.TotalSeconds);

            Position = new Vector2(posX, posY);
        }

        public override void Draw(GameTime elapsedTime)
        {
            spriteBatch.DrawCircle(Position, 1  *Radius / 4, 8, Color.Yellow * darkness, 1.0f);
            spriteBatch.DrawCircle(Position, 2 * Radius / 4, 8, Color.Orange * darkness, 2.0f);
            spriteBatch.DrawCircle(Position, 3 * Radius / 4, 8, Color.OrangeRed * darkness, 3.0f);
            spriteBatch.DrawCircle(Position, 4 * Radius / 4, 8, Color.Red * darkness, 4.0f);
        }

        #endregion

    }
}
