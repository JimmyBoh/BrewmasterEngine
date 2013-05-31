using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Framework;

namespace BrewmasterEngine.Graphics.Content
{
    /// <summary>
    /// Gives global acces to any Texture2D, SpriteFont, etc. Also allows for caching of generated Texture2D's.
    /// </summary>
    public static class ContentHandler
    {
        #region Constructor

        static ContentHandler()
        {
            contents = new Dictionary<string, object>();
        }

        #endregion

        #region Properties

        private static readonly Dictionary<string, object> contents;

        #endregion

        #region Methods

        public static bool Contains(string name)
        {
            return contents.ContainsKey(name);
        }

        public static void Preload<T>(IEnumerable<string> contentPath)
        {
            foreach (var path in contentPath)
            {
                CurrentGame.Content.Load<T>(path);
            }
        }

        /// <summary>
        /// Loads content from a file and adds it to the cache.
        /// </summary>
        /// <typeparam name="T">The type of content.</typeparam>
        /// <param name="contentPath">The path of the content relative to Content.RootPath</param>
        /// <returns>The stored name of the content.</returns>
        public static string Load<T>(string contentPath) where T : class
        {
            if (!contents.ContainsKey(contentPath))
                contents.Add(contentPath, CurrentGame.Content.Load<T>(contentPath));

            return contentPath;
        }

        /// <summary>
        /// Adds generated content to the cache.
        /// </summary>
        /// <typeparam name="T">The type of content.</typeparam>
        /// <param name="name">The unique name of the content.</param>
        /// <param name="content">The content to be stored.</param>
        /// <returns>The stored name of the content.</returns>
        public static string Load<T>(string name, T content) where T : class
        {
            if (!contents.ContainsKey(name))
                contents.Add(name, content);

            return name;
        }

        /// <summary>
        /// Loads content from a file and adds it to the cache.
        /// </summary>
        /// <typeparam name="T">The type of content.</typeparam>
        /// <param name="contentPath">The path of the content relative to Content.RootPath</param>
        /// <returns>The stored name of the content.</returns>
        public static string Overwrite<T>(string contentPath) where T : class
        {
            if (contents.ContainsKey(contentPath))
                contents[contentPath] = CurrentGame.Content.Load<T>(contentPath);
            else
                contents.Add(contentPath, CurrentGame.Content.Load<T>(contentPath));

            return contentPath;
        }

        /// <summary>
        /// Adds generated content to the cache, overwriting the previous content.
        /// </summary>
        /// <typeparam name="T">The type of content.</typeparam>
        /// <param name="name">The name of the content.</param>
        /// <param name="content">The content to be stored.</param>
        /// <returns>The stored name of the content.</returns>
        public static string Overwrite<T>(string name, T content) where T : class
        {
            if (contents.ContainsKey(name))
                contents[name] = content;
            else
                contents.Add(name, content);

            return name;
        }

        public static T[] Contents<T>() where T : class
        {
            return contents.Where(c => c.Value is T).Select(c => c.Value as T).ToArray();
        }

        public static T Retrieve<T>(string name) where T : class
        {
            if(contents.ContainsKey(name)) return contents[name] as T;

            try
            {
                Load<T>(name);
                return Retrieve<T>(name);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

    }
}
