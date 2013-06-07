using System;
using System.Linq;
using BrewmasterEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace SampleGame.Menu.Widgets
{
    public class MenuButton : MenuText
    {
        #region Constructor

        public MenuButton(string text, Vector2 position, Action<MenuButton, bool> onUp, Action<MenuButton> onDown = null) : base(text, position)
        {
            ZIndex = 9;
            OnUp = onUp;
            OnDown = onDown;
            Rotation = CurrentGame.Random.Next(-3, 3)/100.0f;
            ForegroundColor = Color.DarkSlateGray;

            positionAspectRatio = position / CurrentGame.Window.GetSize();
            CurrentGame.Window.ClientSizeChanged += OnWindowResize;
        }

        #endregion

        #region Properties

        public Action<MenuButton, bool> OnUp { get; set; }
        public Action<MenuButton> OnDown { get; set; }
        
        private bool wasClicked;
        private readonly Vector2 positionAspectRatio;

        #endregion

        #region Methods

        public override void Update(GameTime gameTime)
        {
            if (CurrentGame.IsPaused && !Tags.Contains("menu")) return;

            if (!CurrentGame.TouchState.Any()) return;
            
            var touch = CurrentGame.TouchState[0];
            if (touch.State == TouchLocationState.Pressed)
            {
                if (OnDown != null && Bounds.Contains((int) touch.Position.X, (int) touch.Position.Y))
                {
                    OnDown(this);
                    wasClicked = true;
                }
            }
            else if (touch.State == TouchLocationState.Released)
            {
                if (OnUp != null)
                {
                    var releasedOn = Bounds.Contains((int) touch.Position.X, (int) touch.Position.Y);
                    OnUp(this, releasedOn && wasClicked);
                }

                wasClicked = false;
            }
        }

        public override void Draw(GameTime elapsedTime)
        {
            var newBounds = new Rectangle(Bounds.X + 5, Bounds.Y + 5, Bounds.Width, Bounds.Height);
            spriteBatch.FillRectangle(newBounds, Color.Gray * 0.5f, Rotation);
            spriteBatch.FillRectangle(Bounds, Color.DarkGray, Rotation);
            spriteBatch.Draw(this);
        }

        #endregion

        #region Events

        public void OnWindowResize(object o, EventArgs e)
        {
            Position = CurrentGame.Window.GetSize()*positionAspectRatio;
        }

        #endregion

        
    }
}
