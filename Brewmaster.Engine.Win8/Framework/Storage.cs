using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace Brewmaster.Engine.Win8.Framework
{
    public static class Storage
    {
        static Storage()
        {
            
        }

        #region Cloud

        public static void RoamingSettings<T>(string key, T value) where T : class
        {
            ApplicationData.Current.RoamingSettings.Values[key] = value;
        }

        public static T RoamingSettings<T>(string key) where T : class
        {
            return ApplicationData.Current.RoamingSettings.Values[key] as T;
        }

        #endregion

        #region File IO

        public static bool FileExists(string filename)
        {
            var existTask = fileExists(filename);
            existTask.Wait();

            return existTask.Result;
        }

        private static async Task<bool> fileExists(string filename)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;

            try
            {
                var files = storageFolder.GetFilesAsync();

                await storageFolder.GetFileAsync(filename);
                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static async void WriteFile(string filename, string contents)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var file = await storageFolder.GetFileAsync(filename) ?? await storageFolder.CreateFileAsync(filename);

            FileIO.WriteTextAsync(file, contents);
        }

        public static string ReadFile(string filename)
        {
            var readTask = readFile(filename);
            readTask.Wait();

            return readTask.Result;
        }

        private static async Task<string> readFile(string filename)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var file = await storageFolder.GetFileAsync(filename);
            var contents = await FileIO.ReadTextAsync(file);

            return contents;
        }

        public static string[] ReadLines(string filename)
        {
            var linesTask = readLines(filename);
            linesTask.Wait();

            return linesTask.Result.ToArray();
        }

        public static void ReadLines(string filename, Action<string> iterator)
        {
            foreach (var line in ReadLines(filename))
                iterator(line);
        }

        private static async Task<IList<string>> readLines(string filename)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var file = await storageFolder.GetFileAsync(filename);
            var contents = await FileIO.ReadLinesAsync(file);

            return contents;
        }

        #endregion
    }
}
