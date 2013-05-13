using BrewmasterEngine.Extensions;
using BrewmasterEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.Graphics.Core
{
    public abstract class Sprite2D : Sprite
    {
        #region Constructor

        protected Sprite2D(string texturePath, string name = "")
            : base(name)
        {
            Texture = CurrentGame.Content.Load<Texture2D>(texturePath);

            Origin = Bounds.Center.ToVector2();
        }

        #endregion

        #region Properties

        public Texture2D Texture { get; set; }

        public override Vector2 Size
        {
            get { return new Vector2(Texture.Width, Texture.Height); }
        }

        #endregion
    }
}
