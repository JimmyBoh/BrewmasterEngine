using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Framework;
using BrewmasterEngine.GUI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brewmaster.Sample.Win8.Scenes.MainMenu.Entities
{
    public class DemoButton : Button
    {
        public DemoButton(string text, string sceneName)
        {
            Text = text;
            SceneName = sceneName;

            Rotation = CurrentGame.Random.Next(-3, 3)/100.0f;

            RenderAction = (spriteBatch, time) =>
                {
                    var newBounds = new Rectangle(Bounds.X + 5, Bounds.Y + 5, Bounds.Width, Bounds.Height);
                    spriteBatch.FillRectangle(newBounds, Color.Gray*0.5f, Rotation);
                    spriteBatch.FillRectangle(Bounds, Color.DarkGray, Rotation);
                };

            PressAction = onPress;
        }

        public string Text { get; set; }
        public string SceneName { get; set; }
        public float Rotation { get; set; }

        private float scale;
        public float Scale { get { return scale; }set
        {
            Bounds = new Rectangle();
        }}

        private void onPress()
        {
            Scale = 0.9f;
        }
    }
}
