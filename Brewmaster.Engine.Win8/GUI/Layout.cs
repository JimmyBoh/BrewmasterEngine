using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Brewmaster.Engine.Win8.Framework;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Framework;
using BrewmasterEngine.GUI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Windows.Data.Xml.Dom;

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
                
            updateTouchInput(gameTime);
            updateGestureInput(gameTime);
            
            root.Update(gameTime);
        }

        private void updateTouchInput(GameTime gameTime)
        {
            foreach (var touch in CurrentGame.TouchState)
            {
                switch (touch.State)
                {
                    case TouchLocationState.Pressed:

                        break;
                    case TouchLocationState.Moved:

                        break;
                    case TouchLocationState.Released:

                        break;
                }
            }
        }

        private void updateGestureInput(GameTime gameTime)
        {
            foreach (var gesture in CurrentGame.Gestures)
            {
                switch (gesture.GestureType)
                {
                    case GestureType.VerticalDrag:

                        break;
                    case GestureType.DragComplete:

                        break;

                    case GestureType.Flick:

                        break;

                    case GestureType.Hold:

                        break;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (root != null) 
                root.Render(spriteBatch, gameTime);
        }

        #endregion

    }
}
