using System;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SampleGame.Entities
{
    public struct Ball : IPoolable, IGameObject
    {
        #region Properties

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public int ZIndex { get; set; }

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

        public Vector2 PrevPosition { get; set; }
        public Vector2 Position
        {
            get { return position; }
            set
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

        public Vector2 Velocity { get; set; }
        private float acceleration;
        private float maxSpeed;
        private float darkness;

        #endregion

        #region Methods

        

        public void Update(GameTime gameTime)
        {
            if (CurrentGame.IsPaused) return;

            PrevPosition = position;

            if (Math.Abs(Velocity.X).Equals(maxSpeed) && Math.Abs(Velocity.Y).Equals(maxSpeed) && acceleration.Equals(-1.1f))
                acceleration = -0.9f;
            else if (Math.Abs(Velocity.X) < 200.0f && Math.Abs(Velocity.Y) < 200.0f && acceleration.Equals(-0.9f))
                acceleration = -1.1f;

            updateX();
            updateY();

            Position += Velocity*(float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void updateX()
        {
            var velX = Velocity.X;
            var posX = Position.X;

            if (Bounds.Left < 0 || Bounds.Right > CurrentGame.Window.ClientBounds.Width)
            {
                velX *= acceleration;
                velX = MathHelper.Clamp(velX, -maxSpeed, maxSpeed);

                if (Bounds.Left < 0)
                    posX = radius;
                else
                    posX = CurrentGame.Window.ClientBounds.Width - Radius;
            }

            Velocity = new Vector2(velX, Velocity.Y);
            Position = new Vector2(posX, Position.Y);
        }

        private void updateY()
        {
            var velY = Velocity.Y;
            var posY = Position.Y;

            if (Bounds.Top < 0 || Bounds.Bottom > CurrentGame.Window.ClientBounds.Height)
            {
                velY *= acceleration;
                velY = MathHelper.Clamp(velY, -maxSpeed, maxSpeed);

                if (Bounds.Top < 0)
                    posY = radius;
                else
                    posY = CurrentGame.Window.ClientBounds.Height - Radius;
            }

            Velocity = new Vector2(Velocity.X, velY);
            Position = new Vector2(Position.X, posY);
        }

        public void Reverse()
        {
            var deltaX = Velocity.X;
            var deltaY = Velocity.Y;

            deltaX *= acceleration;
            deltaX = MathHelper.Clamp(deltaX, -maxSpeed, maxSpeed);

            deltaY *= acceleration;
            deltaY = MathHelper.Clamp(deltaY, -maxSpeed, maxSpeed);

            Velocity = new Vector2(deltaX, deltaY);
        }

        public void Draw(GameTime elapsedTime)
        {
            CurrentGame.SpriteBatch.DrawCircle(Position, 1 * Radius / 4, 8, Color.Yellow * darkness, 1.0f);
            CurrentGame.SpriteBatch.DrawCircle(Position, 2 * Radius / 4, 8, Color.Orange * darkness, 2.0f);
            CurrentGame.SpriteBatch.DrawCircle(Position, 3 * Radius / 4, 8, Color.OrangeRed * darkness, 3.0f);
            CurrentGame.SpriteBatch.DrawCircle(Position, 4 * Radius / 4, 8, Color.Red * darkness, 4.0f);
        }

        #endregion

        #region IPoolable

        public bool IsFree { get; set; }

        public void Reset()
        {
            acceleration = -1.1f;
            Radius = CurrentGame.Random.Next(4, 32);
            ZIndex = (int) radius - 32;
            maxSpeed = (float) Math.Pow(Radius - 3, 2);
            darkness = radius/32;

            Position = CurrentGame.Window.ClientBounds.GetRandomPoint();

            var dirX = CurrentGame.Random.Next(0, 2);
            if (dirX == 0)
                dirX = -1;

            var dirY = CurrentGame.Random.Next(-1, 2);
            if (dirY == 0)
                dirY = -1;

            Velocity = new Vector2(CurrentGame.Random.Next(100, 500)*dirX, CurrentGame.Random.Next(100, 500)*dirY);
        }

        #endregion

    }
}
