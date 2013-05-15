﻿using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrewmasterEngine.Extensions
{
    public static class XnaExtenions
    {
        #region Point and Mouse.POINT

        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        #endregion

        #region Rectangle and Vector2

        public static Vector2 GetRandomPoint(this Rectangle rect)
        {
            return new Vector2(CurrentGame.Random.Next(rect.Width), CurrentGame.Random.Next(rect.Height));
        }

        public static bool Contains(this Rectangle rect, Vector2 point)
        {
            return rect.Contains((int)point.X, (int)point.Y);
        }

        public static Point ToPoint(this Vector2 point)
        {
            return new Point((int)point.X, (int)point.Y);
        }

        public static Vector2 Herd(this Vector2 pos, Rectangle rect)
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

        #endregion

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
            spriteBatch.Draw(sprite2D.Texture, sprite2D.Position, null, sprite2D.ForegroundColor, sprite2D.Rotation, sprite2D.Origin, sprite2D.Scale, sprite2D.SpriteEffect, 0);
        }

        public static void Draw(this SpriteBatch spriteBatch, SpriteText spriteText)
        {
            if (spriteText.BackgroundColor != Color.Transparent)
            {
                var center = CurrentGame.Window.GetCenter();
                var bgOffset = new Vector2(spriteText.Position.X < center.X ? -1 : 1, spriteText.Position.Y < center.Y ? -1 : 1);

                spriteBatch.DrawString(spriteText.Font, spriteText.Text, spriteText.Position + bgOffset, spriteText.BackgroundColor, spriteText.Rotation, spriteText.Origin, spriteText.Scale, spriteText.SpriteEffect, 0);
            }

            spriteBatch.DrawString(spriteText.Font, spriteText.Text, spriteText.Position, spriteText.ForegroundColor, spriteText.Rotation, spriteText.Origin, spriteText.Scale, spriteText.SpriteEffect, 0);
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
    }
}
