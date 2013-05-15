using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.Framework
{
    public abstract class GameObject
    {
        #region Constructor

        protected GameObject(string name = "")
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                name = "GameObject_" + CurrentGame.GetNextObjectID();

            Name = name;
            IsActive = true;
            IsVisible = true;
            SpriteEffect = SpriteEffects.None;
        }

        #endregion

        #region Properties

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public SpriteEffects SpriteEffect { get; set; }

        protected SpriteBatch spriteBatch
        {
            get{ return CurrentGame.SpriteBatch; }
        }

        #endregion

        #region Methods

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime elapsedTime);

        public override string ToString()
        {
            return this.Name;
        }

        #endregion
    }
}
