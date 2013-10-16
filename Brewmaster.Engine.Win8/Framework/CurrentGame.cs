using System;
using System.Collections.Generic;
using System.Globalization;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace BrewmasterEngine.Framework
{
    /// <summary>
    /// Collection of global game variables.
    /// </summary>
    public static class CurrentGame
    {
        #region Constructor

        static CurrentGame()
        {
            DebugMode = false;
            nextObjectID = 0;
            Random = new Random();
            Gestures = new List<GestureSample>();
        }

        #endregion

        /// <summary>
        /// Shared Random instance
        /// </summary>
        public static Random Random { get; private set; }

        public static bool DebugMode { get; set; }
        
        private static Game2D Game;
        public static void SetGame(Game2D engine)
        {
            Game = engine;
            ScreenSize = WindowSize = Game.Window.ClientBounds.GetSize();
        }

        /// <summary>
        /// The SceneManager that handles the current scene and switching states.
        /// </summary>
        public static SceneManager SceneManager
        {
            get { return Game.SceneManager; }
        }

        /// <summary>
        /// The currently loaded scene.
        /// </summary>
        public static Scene CurrentScene
        {
            get { return SceneManager.CurrentScene; }
        }

        /// <summary>
        /// Gets the current scene cast as a particular scene type.
        /// </summary>
        /// <typeparam name="T">The specific scene type.</typeparam>
        /// <returns>The specific scene or null if the cast failed.</returns>
        public static T CurrentSceneAs<T>() where T: Scene
        {
            if (SceneManager.CurrentScene is T)
                return (T) SceneManager.CurrentScene;

            return null;
        }

        /// <summary>
        /// Whether the current scene is paused of not.
        /// </summary>
        public static bool IsPaused
        {
            get { return CurrentScene.IsPaused; }
        }

        /// <summary>
        /// Pauses the current scene.
        /// </summary>
        public static void PauseCurrentScene()
        {
            CurrentScene.PauseScene();
        }

        /// <summary>
        /// Unpauses the current scene.
        /// </summary>
        public static void UnpauseCurrentScene()
        {
            CurrentScene.UnpauseScene();
        }

        /// <summary>
        /// The XNA Window object to get client bounds and resize events.
        /// </summary>
        public static GameWindow Window
        {
            get { return Game.Window; }
        }

        /// <summary>
        /// The XNA SpriteBatch used for 2D drawing.
        /// </summary>
        public static SpriteBatch SpriteBatch
        {
            get { return Game.SpriteBatch; }
        }

        /// <summary>
        /// The XNA Content object used to load textures, fonts, and sounds.
        /// </summary>
        public static ContentManager Content
        {
            get { return Game.Content; }
        }



        /// <summary>
        /// Current GameTime for the Update loop.
        /// </summary>
        public static GameTime GameTime { get; set; }

        /// <summary>
        /// Collection of raw touch data.
        /// </summary>
        public static TouchCollection TouchState { get; set; }

        /// <summary>
        /// Collection of processed touch data.
        /// </summary>
        public static List<GestureSample> Gestures { get; set; }

        /// <summary>
        /// Max texture size supported by the current device.
        /// </summary>
        public static int MaxTextureSize
        {
            get
            {
                if (maxTextureSize == 0)
                    maxTextureSize = CalculateMaxTextureSize();

                return maxTextureSize;
            }
        }

        private static int maxTextureSize;

        /// <summary>
        /// Determines the maximum texture size through trial and error(catching).
        /// </summary>
        /// <returns>The maximum size for a Texture2D.</returns>
        private static int CalculateMaxTextureSize()
        {
            var maxSize = 0;
            var i = 8;
            while (i < 20)
            {
                try
                {
                    var size = (int) Math.Pow(2, i);
                    new Texture2D(SpriteBatch.GraphicsDevice, size, 1);
                    maxSize = size;

                    size++;
                    new Texture2D(SpriteBatch.GraphicsDevice, size, 1);
                    maxSize = size;
                }
                catch (Exception)
                {
                    break;
                }
                i++;
            }
            return maxSize;
        }

        /// <summary>
        /// Continuous counter to ensure that no two GameObjects have the same default ID.
        /// </summary>
        /// <returns>An ID number as a string.</returns>
        public static string GetNextObjectID()
        {
            return (nextObjectID++).ToString(CultureInfo.InvariantCulture);
        }

        private static int nextObjectID;

        /// <summary>
        /// Dimensions for the screen.
        /// </summary>
        public static Vector2 ScreenSize { get; private set; }

        /// <summary>
        /// Dimensions for the current game window.
        /// </summary>
        public static Vector2 WindowSize { get; set; }
    }
}
