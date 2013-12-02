using System;
using System.Linq;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brewmaster.Sample.Win8.Scenes.BouncingBall.Entities
{
    public class BallManager : GameObject
    {
        #region Constructor

        public BallManager(int initialBalls = 0)
        {
            balls = new ObjectPool<Ball>();

            ballCount = initialBalls;
        }

        #endregion

        #region Properties

        private readonly ObjectPool<Ball> balls;
        private int ballCount;

        public int TotalBalls
        {
            get { return balls.ActiveCount; }
        }

        #endregion

        public override void Update(GameTime gameTime)
        {
            if (balls.ActiveCount == 0 && ballCount < 0)
                ballCount = 0;

            balls.ForEach((ball) =>
                {
                    if (ballCount < 0)
                    {
                        ball.IsFree = true;
                        ballCount++;
                    }
                    else if(!CurrentGame.IsPaused)
                    {
                        ball.Update(gameTime);
                    }

                    return ball;
                });

            if (ballCount > 0)
            {
                // Add a maximum of 25 balls per update.
                var required = Math.Min(ballCount, 25);

                for (var i = 0; i < required; i++)
                    balls.Add(balls.GetNew());

                ballCount -= required;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            balls.ForEach((ball) =>
            {
                ball.Draw(gameTime);
                return ball;
            });
        }

        public void AdjustBallCount(int count)
        {
            ballCount += count;

            if (ballCount < 0 && balls.ActiveCount == 0)
                ballCount = 0;
        }

        public void Clear()
        {
            ballCount = Math.Max(ballCount, 0);
            balls.Clear();
        }
    }
}
