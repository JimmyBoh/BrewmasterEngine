using System;
using System.Linq;
using BrewmasterEngine.DataTypes;
using Microsoft.Xna.Framework;

namespace SampleGame.Scenes.BouncingBall.Entities
{
    public class BallManager : GameObject
    {
        #region Constructor

        public BallManager()
        {
            balls = new ObjectPool<Ball>();
        }

        #endregion

        private readonly ObjectPool<Ball> balls;
        public override void Update(GameTime gameTime)
        {
            balls.ForEach((ball) =>
                {
                    ball.Update(gameTime);
                    return ball;
                });
        }

        public void AddBall()
        {
            balls.Add(balls.GetNew());
        }

        public void AddBalls(int count)
        {
            if (count < 0)
                RemoveBalls(-count);
            else if (count > 0)
                for (var i = 0; i < count; i++)
                    AddBall();
        }

        public void RemoveBall()
        {
            RemoveBalls(1);
        }
        
        public void RemoveBalls(int count)
        {
            if (count < 0)
            {
                AddBalls(-count);
            }
            else if (count > 0)
            {
                balls.ForEach((ball) =>
                    {
                        if (count > 0)
                        {
                            ball.IsFree = true;
                            count--;
                        }
                        return ball;
                    });
            }

        }

        public void Clear()
        {
            balls.Clear();
        }

        public override void Draw(GameTime gameTime)
        {
            balls.ForEach((ball) =>
            {
                ball.Draw(gameTime);
                return ball;
            });
        }
    }
}
