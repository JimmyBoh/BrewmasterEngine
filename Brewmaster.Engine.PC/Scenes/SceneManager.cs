using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Debugging;
using BrewmasterEngine.Framework;
using Microsoft.Xna.Framework;

namespace BrewmasterEngine.Scenes
{
    public class SceneManager : GameManager
    {
        #region Constructors

        public SceneManager()
        {
            Scenes = new Dictionary<string, Scene>();
            CurrentSceneName = "";
            DefaultScene = "";
            DebugMode = false;
        }

        #endregion

        #region Properties

        public Dictionary<string, Scene> Scenes { get; set; }
        public string[] SceneNames
        {
            get { return Scenes.Select(s => s.Key).ToArray(); }
        }

        public string DefaultScene { get; set; }
        public string CurrentSceneName { get; private set; }

        public Scene CurrentScene
        {
            get
            {
                var currScene = !string.IsNullOrEmpty(CurrentSceneName) && Scenes.ContainsKey(CurrentSceneName) ? Scenes[CurrentSceneName]
                        : !string.IsNullOrEmpty(DefaultScene) && Scenes.ContainsKey(DefaultScene) ? Scenes[DefaultScene]
                        : null;

                if (currScene == null)
                    throw new Exception("Failed to retrieve current scene.");

                return currScene;
            }
        }

        #endregion

        #region Methods

        public void Load(string sceneName, Action<Scene> callback = null)
        {
            var previousScene = CurrentSceneName;

            if (string.IsNullOrEmpty(sceneName) || string.IsNullOrWhiteSpace(sceneName) || !Scenes.ContainsKey(sceneName))
            {
                Debugger.Log("Tried to load empty scene name: \"" + sceneName + "\"");
                return;
            }

            CurrentSceneName = sceneName;

            if (!string.IsNullOrEmpty(previousScene) && Scenes.ContainsKey(previousScene))
                Scenes[previousScene].Unload();

            CurrentScene.LoadScene((scene) =>
            {
                if (callback != null) callback(scene);
            });
        }

        public void LoadNextScene(Action<Scene> callback = null)
        {
            var nextIndex = (Array.IndexOf(SceneNames, CurrentSceneName) + 1) % SceneNames.Length;
            Load(SceneNames[nextIndex], callback);
        }

        public void LoadDefaultScene(Action<Scene> callback = null)
        {
            Load(DefaultScene, callback);
        }

        public void PauseCurrentScene()
        {
            CurrentScene.PauseScene();
        }
        
        public void UnpauseCurrentScene()
        {
            CurrentScene.UnpauseScene();
        }

        public void TogglePauseCurrentScene()
        {
            if (CurrentScene.IsPaused)
                UnpauseCurrentScene();
            else
                PauseCurrentScene();
        }

        public void AddScene(Scene scene)
        {
            if (Scenes.Count == 0 || string.IsNullOrEmpty(DefaultScene))
                DefaultScene = scene.Name;

            Scenes.Add(scene.Name, scene);
        }

        public void AddScenes(IEnumerable<Scene> scenes)
        {
            foreach (var scene in scenes)
                AddScene(scene);
        }

        public void RemoveScene(string sceneName)
        {
            Scenes.Remove(sceneName);
        }

        public override void Update(GameTime gameTime)
        {
            CurrentScene.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            CurrentScene.Draw(gameTime);
        }

        public override void Unload()
        {
            Scenes.Clear();
            Scenes = null;
            DefaultScene = null;
            CurrentSceneName = null;
        }

        #endregion
    }
}