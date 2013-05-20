using System.Collections.Generic;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.Debugging
{
    /// <summary>
    /// Platform independent debugger console.
    /// </summary>
    public static class DebugConsole
    {
        #region Constructor

        static DebugConsole()
        {
            console = new Queue<string>();
            consoleLimit = 3;
        }

        #endregion

        #region Properties

        private static readonly Queue<string> console;
        private static int consoleLimit;
        public static int ConsoleLimit
        {
            get { return consoleLimit; }
            set
            {
                consoleLimit = value;
                while (console.Count > consoleLimit)
                    console.Dequeue();
            }
        }

        public static Vector2 Position { get; set; }
        public static Vector2 Size { get; set; }

        public static string FontName { get; set; }
        private static SpriteBatch spriteBatch { get { return CurrentGame.SpriteBatch; } }

        private static SpriteFont font { get { return ContentHandler.Retrieve<SpriteFont>(FontName); } }

        #endregion

        #region Methods

        public static void Log(string format, params object[] parameters)
        {
            if (CurrentGame.DebugMode)
                console.Enqueue(string.Format(format, parameters));

            while (console.Count > consoleLimit)
                console.Dequeue();
        }

        public static void Clear()
        {
            console.Clear();
        }

        public static void Update(GameTime gameTime)
        {
            if (!CurrentGame.DebugMode) return;


        }

        public static void Draw(GameTime gameTime)
        {
            if (!CurrentGame.DebugMode) return;

            var rowHeight = console.Count*font.MeasureString(console.Peek()).Y + 5;
            var totalHeight = console.Count*rowHeight;
            
            spriteBatch.FillRectangle(0.0f, CurrentGame.Window.ClientBounds.Height - totalHeight, CurrentGame.Window.ClientBounds.Width*0.33f, totalHeight, Color.Black*0.5f);
            var y = 0.0f;
            foreach (var log in console)
            {
                spriteBatch.DrawString(ContentHandler.Retrieve<SpriteFont>(FontName), log, Position + new Vector2(5, y), Color.Yellow);
                y += rowHeight;
            }
        }

        #endregion

    }
}
