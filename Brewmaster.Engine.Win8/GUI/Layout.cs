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

        public Layout CreateHorizontalLayout(Action<Element> build)
        {
            return createLayout(new HorizontalPanel(1), build);
        }

        public Layout CreateVerticalLayout(Action<Element> build)
        {
            return createLayout(new VerticalPanel(1), build);
        }

        private Layout createLayout(Panel panel, Action<Element> build)
        {
            root = panel;
            root.Bounds = CurrentGame.Window.ClientBounds;
            root.ID = "r";

            build(root);

            CurrentGame.Window.ClientSizeChanged -= reflowOnResize;
            CurrentGame.Window.ClientSizeChanged += reflowOnResize;

            return this;
        }

        private void reflowOnResize(object sender, EventArgs e)
        {
            Reflow();
        }

        public void Reflow()
        {
            root.Bounds = CurrentGame.Window.ClientBounds;
            root.Reflow(root.Bounds);
        }

        public override void Update(GameTime gameTime)
        {
            if (root == null) return;
                           
            root.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (root == null) return;
 
            root.Draw(gameTime);
        }

        #endregion

    }
}
