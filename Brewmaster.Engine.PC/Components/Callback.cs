using System;
using System.Timers;

namespace BrewmasterEngine.Components
{
    public class Callback
    {
        public static IDisposable SetInterval(Action method, int delayInMilliseconds)
        {
            var timer = new Timer(delayInMilliseconds);
            timer.Elapsed += (source, e) => method();

            timer.Enabled = true;
            timer.Start();

            // Returns a stop handle which can be used for stopping
            // the timer, if required
            return timer;
        }

        public static IDisposable SetTimeout(Action method, int delayInMilliseconds)
        {
            var timer = new Timer(delayInMilliseconds);
            timer.Elapsed += (source, e) => method();

            timer.AutoReset = false;
            timer.Enabled = true;
            timer.Start();

            // Returns a stop handle which can be used for stopping
            // the timer, if required
            return timer;
        }
    }
}
