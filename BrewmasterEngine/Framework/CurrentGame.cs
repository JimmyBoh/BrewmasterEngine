using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BrewmasterEngine.Input;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.Framework
{
    public static class CurrentGame
    {
        static CurrentGame()
        {
            DebugMode = false;
            nextObjectID = 0;
        }

        public static bool DebugMode { get; set; }

        private static GameEngine Game;
        public static void SetGame(GameEngine engine)
        {
            Game = engine;
        }

        public static SceneManager SceneManager
        {
            get { return Game.SceneManager; }
        }

        public static InputManager Input
        {
            get { return Game.InputManager; }
        }

        public static GameWindow Window
        {
            get { return Game.Window; }
        }

        public static SpriteBatch SpriteBatch
        {
            get { return Game.SpriteBatch; }
        }

        public static ContentManager Content
        {
            get { return Game.Content; }
        }

        private static int nextObjectID;
        public static string GetNextObjectID()
        {
            return (nextObjectID++).ToString(CultureInfo.InvariantCulture);
        }
    }
}
