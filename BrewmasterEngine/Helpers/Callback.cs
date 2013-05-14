using System;
using System.Timers;

namespace BrewmasterEngine.Helpers
{
    public static class Callback
    {
        /// <summary>
        /// Executes a function on a specified interval.
        /// </summary>
        /// <param name="method">The function to execute.</param>
        /// <param name="delayInMilliseconds">The number of milliseconds between each method call.</param>
        /// <returns>Call `.Dispose()` to cancel the interval.</returns>
        public static IDisposable SetInterval(Action method, int delayInMilliseconds)
        {
            var timer = new Timer(delayInMilliseconds);
            timer.Elapsed += (source, e) => method();

            timer.Enabled = true;
            timer.Start();

            return timer;
        }

        /// <summary>
        /// Executes a function once after a specified interval.
        /// </summary>
        /// <param name="method">The function to execute.</param>
        /// <param name="delayInMilliseconds">The number of milliseconds before the method call.</param>
        /// <returns>Call `.Dispose()` to cancel the timeout.</returns>
        public static IDisposable SetTimeout(Action method, int delayInMilliseconds)
        {
            var timer = new Timer(delayInMilliseconds);
            timer.Elapsed += (source, e) => method();

            timer.AutoReset = false;
            timer.Enabled = true;
            timer.Start();

            return timer;
        }
    }
}
