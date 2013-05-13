using BrewmasterEngine.Extensions;
using BrewmasterEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.Graphics.Core
{
    public abstract class SpriteText : Sprite
    {
        #region Constructor

        protected SpriteText(string fontPath, string text = "", string name = "") : base(name)
        {
            Font = CurrentGame.Content.Load<SpriteFont>(fontPath);
            Text = text;
            Origin = Bounds.Center.ToVector2();
            BackgroundColor = Color.Transparent;
        }

        #endregion

        #region Properties

        public SpriteFont Font { get; set; }
        public string Text { get; set; }
        public Color BackgroundColor { get; set; }

        public override Vector2 Size
        {
            get { return Text != null ? Font.MeasureString(Text) : Vector2.Zero; }
        }

        #endregion
    }
}
