using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.GUI;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brewmaster.Sample.Win8.Scenes.BouncingBall.Entities
{
    public class GameGuiButton : Button
    {
        public GameGuiButton(string text, Action<GameGuiButton> clicked)
        {
            Text = new StaticText("DebugFont", text);

            Clicked = clicked;

            Rotation = CurrentGame.Random.Next(-3, 3) / 100.0f;
            Scale = Vector2.One;
        }

        public StaticText Text { get; set; }

        public Action<GameGuiButton> Clicked { get; set; }

        public float Rotation { get; set; }

        public override void Reflow(Rectangle area)
        {
            Bounds = new Rectangle((int)(Bounds.X + Bounds.Width * 0.25f), (int)(Bounds.Y + Bounds.Height * 0.1f), (int)(Bounds.Width * 0.5f), (int)(Bounds.Height * 0.8f));

            Text.CenterOn(RenderBounds.Center.ToVector2());
        }

        public override void Draw(GameTime gameTime)
        {
            var offset = (int)((1f - Scale.X) * 100f);
            var shadow = new Rectangle(RenderBounds.X + 5 + offset, RenderBounds.Y + 5 + offset, (int)(RenderBounds.Width * Scale.X), (int)(RenderBounds.Height * Scale.X));
            var tile = new Rectangle(RenderBounds.X + offset, RenderBounds.Y + offset, (int)(RenderBounds.Width * Scale.X), (int)(RenderBounds.Height * Scale.X));
            spriteBatch.FillRectangle(shadow, Color.Gray * 0.5f, Rotation);
            spriteBatch.FillRectangle(tile, Color.DarkGray, Rotation);

            Text.Draw(gameTime);
        }

        public override void OnPress()
        {
            alterScale(-0.05f);
        }

        public override void OnDragOut()
        {
            alterScale(0.05f);
        }

        public override void OnRelease()
        {
            alterScale(0.05f);

            if (Clicked != null)
                Clicked(this);
        }

        private void alterScale(float amount)
        {
            Scale = new Vector2(Scale.X + amount);
            Text.Scale += new Vector2(amount);
        }
    }
}
