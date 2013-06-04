using BrewmasterEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.Graphics.Content
{
    public static class Gradient
    {
        public static Texture2D CreateHorizontal(int width)
        {
            return CreateVertical(width, Color.Black, Color.White);
        }

        public static Texture2D CreateHorizontal(int width, Color left, Color right)
        {
            var texture = new Texture2D(CurrentGame.SpriteBatch.GraphicsDevice, width, 1);
            var bgc = new Color[width];

            for (var i = 0; i < bgc.Length; i++)
            {
                var amount = (float)i / width;
                bgc[i] = new Color(Vector4.Lerp(left.ToVector4(), right.ToVector4(), amount));
            }
            texture.SetData(bgc);
            return texture;
        }

        public static Texture2D CreateVertical(int height)
        {
            return CreateVertical(height, Color.Black, Color.White);
        }

        public static Texture2D CreateVertical(int height, Color top, Color bottom)
        {
            var texture = new Texture2D(CurrentGame.SpriteBatch.GraphicsDevice, 1, height);
            var bgc = new Color[height];

            for (var i = 0; i < bgc.Length; i++)
            {
                var amount = (float) i/height;
                bgc[i] = new Color(Vector4.Lerp(top.ToVector4(), bottom.ToVector4(), amount));
            }
            texture.SetData(bgc);
            return texture;
        }
    }
}
