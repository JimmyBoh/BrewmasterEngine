using System;


#if WINDOWS
using System.Timers;
#else
using Windows.UI.Xaml;
#endif

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
        public static void SetInterval(Action method, int delayInMilliseconds)
        {
#if WINDOWS
            var timer = new Timer(delayInMilliseconds);
            timer.Elapsed += (source, e) => method();

            timer.Enabled = true;
            timer.Start();
#else
            DispatcherTimer timer = new DispatcherTimer();

            timer.Tick += (o, e) => method(); 
            timer.Interval = new TimeSpan(00, 1, 1);
            timer.Start();
#endif
        }

        /// <summary>
        /// Executes a function once after a specified interval.
        /// </summary>
        /// <param name="method">The function to execute.</param>
        /// <param name="delayInMilliseconds">The number of milliseconds before the method call.</param>
        /// <returns>Call `.Dispose()` to cancel the timeout.</returns>
        public static void SetTimeout(Action method, int delayInMilliseconds)
        {
#if WINDOWS
            var timer = new Timer(delayInMilliseconds);
            timer.Elapsed += (source, e) => method();

            timer.AutoReset = false;
            timer.Enabled = true;
            timer.Start();
#else
            DispatcherTimer timer = new DispatcherTimer();

            timer.Tick += (o, e) =>
                {
                    timer.Stop();
                    method();
                };
            timer.Interval = new TimeSpan(00, 1, 1);
            timer.Start();
#endif
        }
    }
}
