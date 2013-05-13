﻿using BrewmasterEngine.Extensions;
using BrewmasterEngine.Framework;
using Microsoft.Xna.Framework;

namespace BrewmasterEngine.Graphics.Core
{
    public abstract class Sprite : GameObject
    {
        #region Constructor

        protected Sprite(string name = "") : base(name)
        {
            IsVisible = true;
            Position = Vector2.Zero;
            Rotation = 0.0f;
            Origin = Bounds.Center.ToVector2();
            Scale = Vector2.One;
            ForegroundColor = Color.White;
        }

        #endregion

        #region Properties

        public Vector2 Position { get; set; }
        public abstract Vector2 Size { get; }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)(Position.X - (Origin.X * Scale.X)),
                                  (int)(Position.Y - (Origin.Y * Scale.Y)),
                                  (int)(Size.X * Scale.X),
                                  (int)(Size.Y * Scale.Y));
            }
        }

        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Scale { get; set; }
        public Color ForegroundColor { get; set; }
        
        #endregion

        #region Methods

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime elapsedTime)
        {
            spriteBatch.Draw(this);
        }

        #endregion
    }
}
