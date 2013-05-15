using BrewmasterEngine.Framework;

namespace BrewmasterEngine.Debugging
{
    public static class Debugger
    {
        static Debugger()
        {

        }

        #region Properties

        #endregion

        public static void Log(string format, params object[] param)
        {
            // if this is too slow, start a Task to do it on another thread...
            if (CurrentGame.DebugMode)
                System.Diagnostics.Debug.WriteLine(format, param);
        }

        public static void Log(params object[] objs)
        {
            Log("{0}", objs);
        }
    }
}
