using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Framework;
using BrewmasterEngine.GUI.Elements;
using Microsoft.Xna.Framework;

namespace BrewmasterEngine.GUI
{
    public class GuiManager : GameManager
    {
        public Panel Root { get; set; }

        #region Methods

        public Element CreateHorizontalLayout()
        {
            return createLayout(Layout.Horizontal);
        }

        public Element CreateVerticalLayout()
        {
            return createLayout(Layout.Vertical);
        }

        public Element CreateAbsoluteLayout()
        {
            return createLayout(Layout.Absolute);
        }

        private Element createLayout(Layout layout)
        {
            Root = new Panel(1, layout)
            {
                Bounds = CurrentGame.Window.ClientBounds,
                ID = "r"
            };

            CurrentGame.Window.ClientSizeChanged -= reflowOnResize;
            CurrentGame.Window.ClientSizeChanged += reflowOnResize;

            return Root;
        }

        private void reflowOnResize(object sender, EventArgs e)
        {
            Reflow();
        }

        public void Reflow()
        {
            Root.Bounds = CurrentGame.Window.ClientBounds;
            Root.Reflow();
        }

        public override void Update(GameTime gameTime)
        {
            if (Root == null) return;
                
            Root.Reflow();
            Root.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Root != null) Root.Render(spriteBatch, gameTime);
        }

        public string ToXml()
        {
            return "";
        }

        public override void Unload()
        {
            Root = null;
        }

        #endregion

    }
}
