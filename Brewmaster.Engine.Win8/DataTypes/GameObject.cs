using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.DataTypes
{
    public abstract class GameObject : IGameObject
    {
        #region Constructor

        protected GameObject(string name = "")
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                name = "GameObject_" + CurrentGame.GetNextObjectID();

            Name = name;
            IsActive = true;
            IsVisible = true;
            IsFree = false;
            ZIndex = 0;
        }

        #endregion

        #region Properties

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public bool IsFree { get; set; }
        public int ZIndex { get; set; }

        protected SpriteBatch spriteBatch
        {
            get{ return CurrentGame.SpriteBatch; }
        }

        #endregion

        #region Methods

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);

        public void Hide()
        {
            this.IsVisible = this.IsActive = false;
        }

        public void Show()
        {
            this.IsVisible = this.IsActive = true;
        }

        public override string ToString()
        {
            return this.Name;
        }

        #endregion
    }
}
