using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.Graphics
{
    public abstract class SpriteText : Sprite
    {
        #region Constructor

        protected SpriteText(string fontName, string text = "", string name = "") : base(name)
        {
            this.fontName = fontName;
            this.text = text;
            updateProperties();
            BackgroundColor = Color.Transparent;
        }

        #endregion

        #region Properties

        private string fontName;
        public string FontName
        {
            get { return fontName; }
            set
            {
                fontName = value;
                updateProperties();
            }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                updateProperties();
            }
        }

        public Color BackgroundColor { get; set; }
        private Vector2 size;
        public override Vector2 Size { get { return Text != null ? size : Vector2.Zero; } }

        #endregion

        #region Methods

        private void updateProperties()
        {
            size = ContentHandler.Retrieve<SpriteFont>(fontName).MeasureString(text);
            Origin = (size == Vector2.Zero ? Vector2.Zero : size / 2.0f);
        }

        #endregion
    }
}
