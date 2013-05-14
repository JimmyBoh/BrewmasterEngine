using System;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BrewmasterEngine.Extensions;

namespace SampleGame.Menu.Widgets
{
    public class MenuButton : MenuText, INotPausable
    {
        #region Constructor

        public MenuButton(string text, Vector2 position, Action<MenuButton, bool> onUp, Action<MenuButton> onDown = null) : base(text, position)
        {
            OnUp = onUp;
            OnDown = onDown;
            Rotation = CurrentGame.Random.Next(-3, 3)/100.0f;
        }

        #endregion

        #region Properties

        public Action<MenuButton, bool> OnUp { get; set; }
        public Action<MenuButton> OnDown { get; set; }
        private bool wasClicked;

        #endregion

        #region Methods

        private MouseState prevState;
        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released && Bounds.Contains(mouseState.X, mouseState.Y))
            {
                if (OnDown != null)
                    OnDown(this);

                wasClicked = true;
            }

            if (mouseState.LeftButton == ButtonState.Released && prevState.LeftButton == ButtonState.Pressed)
            {
                if (OnUp != null)
                    OnUp(this, Bounds.Contains(mouseState.X, mouseState.Y) && wasClicked);

                wasClicked = false;
            }

            prevState = mouseState;
        }

        public override void Draw(GameTime elapsedTime)
        {
            var newBounds = new Rectangle(Bounds.X + 5, Bounds.Y + 5, Bounds.Width, Bounds.Height);
            spriteBatch.FillRectangle(newBounds, Color.Gray * 0.5f, Rotation);
            spriteBatch.FillRectangle(Bounds, Color.DarkGray, Rotation);
            spriteBatch.Draw(this);
        }

        #endregion

        public void OnPause()
        {
            // Do nothing...
        }

        public void OnUnpause()
        {
            // Do nothing...
        }
    }
}
