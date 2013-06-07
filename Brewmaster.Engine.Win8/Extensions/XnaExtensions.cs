using System;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics;

namespace Microsoft.Xna.Framework
{
        public static class XnaFrameworkExtensions
        {
            #region Vector2, Point, Rectangle

            public static Vector2 ToVector2(this Point point)
            {
                return new Vector2(point.X, point.Y);
            }

            public static Vector2 GetSize(this Rectangle rect)
            {
                return new Vector2(rect.Width, rect.Height);
            }

            public static Vector2 GetRandomPoint(this Rectangle rect)
            {
                return new Vector2(CurrentGame.Random.Next(rect.Width), CurrentGame.Random.Next(rect.Height));
            }

            public static bool Contains(this Rectangle rect, Vector2 point)
            {
                return rect.Contains((int) point.X, (int) point.Y);
            }

            public static bool Contains(this Rectangle rect, float rotation, Vector2 point)
            {
                var origin = rect.Center.ToVector2();

                if (origin == point) return true;

                var newVector = point - origin;

                var sin = Math.Sin(-rotation);
                var cos = Math.Cos(-rotation);

                var realPoint = new Vector2((float) (newVector.X*cos - newVector.Y*sin), (float) (newVector.X*sin + newVector.Y*cos)) + origin;

                return rect.Contains(realPoint);
            }

            public static Point ToPoint(this Vector2 point)
            {
                return new Point((int) point.X, (int) point.Y);
            }

            public static Vector2 Clamp(this Vector2 pos, Rectangle rect)
            {
                var x = pos.X;
                var y = pos.Y;

                if (x < rect.X)
                    x = rect.X;
                else if (x > rect.Right)
                    x = rect.Right;

                if (y < rect.Y)
                    y = rect.Y;
                else if (y > rect.Bottom)
                    y = rect.Bottom;

                return new Vector2(x, y);
            }

            /// <summary>
            /// The Cross product of two points.
            /// </summary>
            /// <param name="A"></param>
            /// <param name="other"></param>
            /// <param name="LeftCross">Set to true to return the left cross</param> 
            /// <returns>perpendicular directional point to the two vectors</returns> 
            public static Vector2 Cross(this Vector2 thisVector, Vector2 other, bool LeftCross = false)
            {
                if (LeftCross)
                    return new Vector2(+other.Y + thisVector.X, -other.X + thisVector.Y);

                return new Vector2(-other.Y + thisVector.X, +other.X + thisVector.Y);
            }

            #endregion

            #region GameWindow

            public static Vector2 GetCenter(this GameWindow window)
            {
                return window.GetSize() / 2.0f;
            }

            public static Vector2 GetSize(this GameWindow window)
            {
                return new Vector2(window.ClientBounds.Width, window.ClientBounds.Height);
            }

            private static Vector2 initialWindowSize;

            public static Vector2 GetInitialSize(this GameWindow window)
            {
                return initialWindowSize;
            }

            public static void SetInitialSize(this GameWindow window)
            {
                initialWindowSize = window.GetSize();
            }


            //public static void SetPosition(this GameWindow window, Point position)
            //{
            //    var OTKWindow = GetForm(window);
            //    if (OTKWindow == null) return;

            //    OTKWindow.X = position.X;
            //    OTKWindow.Y = position.Y;
            //}

            //public static Point GetPosition(this GameWindow window)
            //{
            //    var OTKWindow = GetForm(window);
            //    return OTKWindow == null ? Point.Zero : new Point(OTKWindow.X, OTKWindow.Y);
            //}

            //private static OpenTK.GameWindow GetForm(this GameWindow gameWindow)
            //{
            //    var field = typeof(OpenTKGameWindow).GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            //    if (field != null)
            //        return field.GetValue(gameWindow) as OpenTK.GameWindow;
            //    return null;
            //}

            #endregion

            #region Colors

            public static Color Randomize(this Color color)
            {
                return new Color((float)CurrentGame.Random.NextDouble(), (float)CurrentGame.Random.NextDouble(), (float)CurrentGame.Random.NextDouble());
            }

            #endregion
        }
}


namespace Microsoft.Xna.Framework.Graphics
{
    public static class XnaFrameworkGraphicsExtensions
    {
        #region SpriteBatch

        public static void Draw(this SpriteBatch spriteBatch, Sprite sprite)
        {
            if (sprite is Sprite2D)
                spriteBatch.Draw(sprite as Sprite2D);
            else if (sprite is SpriteText)
                spriteBatch.Draw(sprite as SpriteText);
        }

        public static void Draw(this SpriteBatch spriteBatch, Sprite2D sprite2D)
        {
            spriteBatch.Draw(ContentHandler.Retrieve<Texture2D>(sprite2D.TextureName), sprite2D.Position, null, sprite2D.ForegroundColor, sprite2D.Rotation, sprite2D.Origin, sprite2D.Scale, sprite2D.SpriteEffect, 0);
        }

        public static void Draw(this SpriteBatch spriteBatch, SpriteText spriteText)
        {
            // Draw the text once as a shadow/background...
            if (spriteText.BackgroundColor != Color.Transparent)
            {
                var center = CurrentGame.Window.GetCenter();
                var bgOffset = new Vector2(spriteText.Position.X < center.X ? -1 : 1, spriteText.Position.Y < center.Y ? -1 : 1);

                spriteBatch.DrawString(ContentHandler.Retrieve<SpriteFont>(spriteText.FontName), spriteText.Text, spriteText.Position + bgOffset, spriteText.BackgroundColor, spriteText.Rotation, spriteText.Origin, spriteText.Scale, spriteText.SpriteEffect, 0);
            }

            // Draw the text for real...
            spriteBatch.DrawString(ContentHandler.Retrieve<SpriteFont>(spriteText.FontName), spriteText.Text, spriteText.Position, spriteText.ForegroundColor, spriteText.Rotation, spriteText.Origin, spriteText.Scale, spriteText.SpriteEffect, 0);
        }

        #endregion

        #region Texture2D

        public static Vector2 Size(this Texture2D texture)
        {
            return new Vector2(texture.Width, texture.Height);
        }

        #endregion
    }
}

namespace Microsoft.Xna.Framework.Input
{
    public static class XnaFrameworkInputExtensions
    {
        public static Vector2 Position(this MouseState mouse)
        {
            return new Vector2(mouse.X, mouse.Y);
        }
    }

}
