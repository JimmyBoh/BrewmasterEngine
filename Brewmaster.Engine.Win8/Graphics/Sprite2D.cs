using BrewmasterEngine.Graphics.Content;
using Microsoft.Xna.Framework;
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

            Size = ContentHandler.Retrieve<Texture2D>(TextureName).Size();
            Origin = Bounds.Center.ToVector2();
        }

        #endregion

        #region Properties

        public string TextureName { get; set; }

        #endregion
    }
}
