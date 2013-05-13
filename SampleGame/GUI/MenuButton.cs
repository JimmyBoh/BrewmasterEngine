using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using BrewmasterEngine.Extensions;

namespace SampleGame.GUI
{
    public class MenuButton : MenuText
    {
        #region Constructor

        public MenuButton(string text, Vector2 position, string targetScene = "", float resize = 1.2f): base(text, position)
        {
            this.TargetScene = targetScene;
            Resize = resize;
            ForegroundColor = Color.Black;
        }

        #endregion

        #region Properties

        public string TargetScene { get; set; }
        public float Resize { get; private set; }
        #endregion

        #region Methods

        private MouseState prevState;
        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            Scale = Bounds.Contains(mouseState.X, mouseState.Y)
                        ? new Vector2(Resize)
                        : Vector2.One;

            if (Bounds.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Released && prevState.LeftButton == ButtonState.Pressed)
                CurrentGame.SceneManager.Load(TargetScene);

            prevState = mouseState;
        }

        public override void Draw(GameTime elapsedTime)
        {
            var newBounds = new Rectangle(Bounds.X + 5, Bounds.Y + 5, Bounds.Width, Bounds.Height);
            spriteBatch.FillRectangle(newBounds, Color.Gray);
            spriteBatch.FillRectangle(Bounds,Color.DarkGray,-0.03f);
            spriteBatch.Draw(this);
        }

        #endregion

    }
}
