using System;
using System.Collections.Generic;
using BrewmasterEngine.DataTypes;
using Microsoft.Xna.Framework;

namespace BrewmasterEngine.Tools
{
    public class Profiler : GameObject
    {
        public Profiler()
        {
            timers = new Dictionary<string, TimeSpan>();
            position = new Vector2(10.0f);
            size = new Vector2(120.0f, 80.0f);

            bgColor = Color.Gray * 0.6f;
        }

        private readonly Dictionary<string, TimeSpan> timers;
        
        private readonly Vector2 position;
        private readonly Vector2 size;
        private readonly Color bgColor;

        #region Methods

        public void Start(string sectionName)
        {
            if (timers.ContainsKey(sectionName))
                timers.Add(sectionName,TimeSpan.Zero);
                
        }

        public void Stop(string sectionName)
        {
            
        }

        public void Stop()
        {

        }

        public override void Update(GameTime gameTime)
        {
            // Do nothing...
        }

        public override void Draw(GameTime gameTime)
        {
            //var totalTime = timers.Sum(t => t.Value.TotalSeconds);

            //// Draw Background
            //spriteBatch.FillRectangle(position, size,bgColor);


            //foreach (var timer in timers)
            //{
            //    var rating = timer.Value.TotalSeconds/totalTime;
            //    var barColor = (rating < 0.1 ? Color.DarkGreen : rating < .5)


            //}


            //timers.Clear();
        }

        #endregion
    }
}
