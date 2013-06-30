using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace BrewmasterEngine.GUI
{
    public abstract class Layout : GameObject
    {
        private Panel root;

        #region Methods

        public void CreateHorizontalLayout(Action<Element> build)
        {
            createLayout(LayoutStyle.Horizontal, build);
        }

        public void CreateVerticalLayout(Action<Element> build)
        {
            createLayout(LayoutStyle.Vertical, build);
        }

        public void CreateAbsoluteLayout(Action<Element> build)
        {
            createLayout(LayoutStyle.Absolute, build);
        }

        private void createLayout(LayoutStyle layoutStyle, Action<Element> build)
        {
            root = new Panel(1, layoutStyle)
            {
                Bounds = CurrentGame.Window.ClientBounds,
                ID = "r"
            };

            build(root);

            CurrentGame.Window.ClientSizeChanged -= reflowOnResize;
            CurrentGame.Window.ClientSizeChanged += reflowOnResize;
        }

        private void reflowOnResize(object sender, EventArgs e)
        {
            Reflow();
        }

        public void Reflow()
        {
            root.Bounds = CurrentGame.Window.ClientBounds;
            root.Reflow();
        }

        public override void Update(GameTime gameTime)
        {
            if (root == null) return;
                           
            root.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (root != null) 
                root.Draw(gameTime);
        }

        #endregion

    }
}
