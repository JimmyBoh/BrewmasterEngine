using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.GUI.Elements
{
    public abstract class Button : Element
    {
        #region Constructor

        protected Button()
        {
            wasPressed = false;
        }

        #endregion

        public Action PressAction { get; set; }
        public Action<bool> ReleaseAction { get; set; }
        public Action<SpriteBatch,GameTime> RenderAction { get; set; }

        private bool wasPressed;

        #region Methods

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (RenderAction != null)
                RenderAction(spriteBatch, gameTime);
        }

        #endregion

        public override void OnPress(Vector2 position)
        {
            if (PressAction == null) return;
            
            wasPressed = true;
            PressAction();
        }

        public override void OnRelease(Vector2 position)
        {
            if (ReleaseAction == null || !wasPressed) return;
            
            wasPressed = false;
            ReleaseAction(RenderBounds.Contains(position.ToPoint()));
        }

    }
}
