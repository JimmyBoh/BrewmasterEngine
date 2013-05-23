using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.DataTypes
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
            IsDestroyed = false;
            ZIndex = 0;
            tags = new List<string>();
        }

        #endregion

        #region Properties

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public bool IsDestroyed { get; set; }
        public int ZIndex { get; set; }

        private readonly List<string> tags;
        public List<string> Tags
        {
            get { return tags.ToList(); }
        }

        public string[] AddTags
        {
            set { tags.AddRange(value); }
        }

        public string[] RemoveTags
        {
            set
            {
                foreach (var flag in value)
                {
                    tags.Remove(flag);
                }
            }
        }

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
