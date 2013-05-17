using BrewmasterEngine.Extensions;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.Graphics
{
    public abstract class Sprite2D : Sprite
    {
        #region Constructor

        protected Sprite2D(string textureName, string objectName = "") : base(objectName)
        {
            ContentHandler.Load<Texture2D>(textureName);
            TextureName = textureName;

            Origin = Bounds.Center.ToVector2();
        }

        #endregion

        #region Properties

        public string TextureName { get; set; }

        public override Vector2 Size
        {
            get { return ContentHandler.Retrieve<Texture2D>(TextureName).Size(); }
        }

        #endregion
    }
}
