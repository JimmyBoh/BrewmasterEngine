using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Graphics;
using Microsoft.Xna.Framework;

namespace BrewmasterEngine.GUI
{
    public class Panel : Element
    {
        #region Constructor

        public Panel(string name, Vector2 position, Vector2 size, Layout layout = Layout.None) : base(name)
        {
            Position = position;
            Size = size;
            Layout = layout;

            Children = new GameObjectCollection();
        }

        #endregion

        #region Properties

        public GameObjectCollection Children { get; set; }
        public Layout Layout { get; set; }
        private bool IsChanged;

        #endregion

        #region Methods

        public void AddElement(Element element)
        {
            Children.Add(element);
        }

        public void RemoveElement(string name)
        {
            Children.Remove(name);
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            
        }

        #endregion
    }
}
