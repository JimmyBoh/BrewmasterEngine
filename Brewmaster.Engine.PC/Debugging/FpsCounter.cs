using System;
using BrewmasterEngine.Graphics;
using BrewmasterEngine.Extensions;
using Microsoft.Xna.Framework;

namespace BrewmasterEngine.Debugging
{
    public class FpsCounter : SpriteText
    {
        public FpsCounter(string fontPath) : base(fontPath, "fpsCounter")
        {
            Position = new Vector2(70, 20);
            ForegroundColor = Color.Red;
            BackgroundColor = Color.DarkRed;

            _frameRate = 0;
            _frameCounter = 0;
            _elapsedTime = TimeSpan.Zero;
        }

        private int _frameRate;
        private int _frameCounter;
        private TimeSpan _elapsedTime;

        public override void Update(GameTime gameTime)
        {
            _elapsedTime += gameTime.ElapsedGameTime;

            if (_elapsedTime <= TimeSpan.FromSeconds(1)) return;
            _elapsedTime -= TimeSpan.FromSeconds(1);
            _frameRate = _frameCounter;
            _frameCounter = 0;
        }

        public override void Draw(GameTime elapsedTime)
        {
            _frameCounter++;

            if (_frameRate < 15)
            {
                ForegroundColor = Color.Red;
                BackgroundColor = Color.DarkRed;
            }
            else if (_frameRate < 30)
            {
                ForegroundColor = Color.Goldenrod;
                BackgroundColor = Color.DarkGoldenrod;
            }
            else
            {
                ForegroundColor = Color.Green;
                BackgroundColor = Color.DarkGreen;
            } 

            Text = string.Format("FPS:{0:0#}", _frameRate);
            spriteBatch.FillRectangle(Bounds, Color.Black*0.5f);
            spriteBatch.Draw(this);
        }
    }
}
