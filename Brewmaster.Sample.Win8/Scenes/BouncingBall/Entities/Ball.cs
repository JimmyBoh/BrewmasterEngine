using System;
using System.Linq;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brewmaster.Sample.Win8.Scenes.BouncingBall.Entities
{
    public struct Ball : IPoolable, IGameObject
    {
        #region Constants

        private const float GRAVITY = 500f;
        private const float PULL_SPEED = 1000000f;
        private const int MAX_RADIUS = 16;
        private const float MAX_SPEED = 1000f;

        #endregion

        #region Properties

        public string TextureName { get; set; }
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

        public float Scale { get; private set; }

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
            Scale = (2f * Radius) / ContentHandler.Retrieve<Texture2D>(TextureName).Width;
            Bounds = new Rectangle((int) (Position.X - radius), (int) (Position.Y - radius),
                                   (int) radius*2, (int) radius*2);
        }

        public Vector2 Velocity { get; set; }
        public Color Color { get; set; }
        private float darkness;

        #endregion

        #region Methods

        

        public void Update(GameTime gameTime)
        {
            if (CurrentGame.IsPaused) return;

            PrevPosition = position;

            updateX();
            updateY();

            updateTouch();

            if (Velocity.Length() > MAX_SPEED)
            {
                Velocity = Vector2.Normalize(Velocity)*MAX_SPEED;
            }

            Position += Velocity*(float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void updateX()
        {
            var velX = Velocity.X;
            var posX = Position.X;

            if (Bounds.Left < 0 || Bounds.Right > CurrentGame.Window.ClientBounds.Width)
            {
                velX *= -1f + (CurrentGame.Random.Next(-100,100)/200f);

                if (Bounds.Left < 0)
                    posX = radius;
                else
                    posX = CurrentGame.Window.ClientBounds.Width - Radius;
            }

            if (Math.Abs(velX) < 0.9f)
                velX = 0;

            Velocity = new Vector2(velX, Velocity.Y);
            Position = new Vector2(posX, Position.Y);
        }

        private void updateY()
        {
            var velY = Velocity.Y;
            var velX = Velocity.X;
            var posY = Position.Y;

            if (Bounds.Top < 0 || Bounds.Bottom > CurrentGame.Window.ClientBounds.Height)
            {
                velY *= -0.67f + (CurrentGame.Random.Next(-100, 100) / 200f);
                velX *= 0.80f + (CurrentGame.Random.Next(-100, 100) / 200f);
                

                if (Bounds.Top < 0)
                    posY = radius;
                else
                    posY = CurrentGame.Window.ClientBounds.Height - Radius;
            }
            
            
            velY += GRAVITY * (float)CurrentGame.GameTime.ElapsedGameTime.TotalSeconds;

            if (Math.Abs(velY) < 5f)
                velY = 0;

            if (Math.Abs(velX) < 5f)
                velX = 0;


            Velocity = new Vector2(velX, velY);
            Position = new Vector2(Position.X, posY);
        }

        private void updateTouch()
        {
            if (!CurrentGame.TouchState.Any()) return;

            foreach (var touch in CurrentGame.TouchState)
            {
                var pull = new Vector2(touch.Position.X - Position.X, touch.Position.Y - Position.Y);
                var dist = pull.LengthSquared();

                if (dist > radius)
                {
                    pull = Vector2.Normalize(pull) * PULL_SPEED / dist;
                    Velocity += pull;
                }
            }
        }

        public void Draw(GameTime elapsedTime)
        {
            CurrentGame.SpriteBatch.Draw(ContentHandler.Retrieve<Texture2D>(TextureName), Bounds, Color*darkness);
        }

        #endregion

        #region IPoolable

        public bool IsFree { get; set; }

        public void Reset()
        {
            TextureName = "Ball";
            Radius = CurrentGame.Random.Next(4, MAX_RADIUS);
            ZIndex = (int) radius - 32;
            Color = new Color((float)CurrentGame.Random.NextDouble(), (float)CurrentGame.Random.NextDouble(), (float)CurrentGame.Random.NextDouble());
            darkness = radius/16.0f;

            Position = new Vector2(CurrentGame.Window.ClientBounds.GetRandomPoint().X, CurrentGame.Window.ClientBounds.Height - CurrentGame.Random.Next(50, 100));

            var dirX = CurrentGame.Random.Next(0, 2);
            if (dirX == 0)
                dirX = -1;

            var dirY = CurrentGame.Random.Next(-1, 2);
            if (dirY == 0)
                dirY = -1;

            Velocity = new Vector2(CurrentGame.Random.Next(10, 100)*dirX, CurrentGame.Random.Next(10, 100)*dirY);
        }

        #endregion

    }
}
