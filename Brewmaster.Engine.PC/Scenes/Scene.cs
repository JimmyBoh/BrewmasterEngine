using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Debugging;
using Microsoft.Xna.Framework;


namespace BrewmasterEngine.Scenes
{
    public abstract class Scene
    {
        #region Constructors

        protected Scene(string name)
        {
            Name = name;
            entities = new GameObjectCollection();
        }

        #endregion

        #region Properties

        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsPaused { get; private set; }

        private readonly GameObjectCollection entities;
        public GameObject[] Entities { get { return entities.ToArray(); } }

        #endregion

        #region Methods

        public void Add(GameObject gameObject)
        {
            entities.Add(gameObject);
        }

        public void Add(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
                Add(gameObject);
        }

        public void Remove(string objectName)
        {
            entities.Remove(objectName);
        }

        public void Remove(GameObject gameObject)
        {
            Remove(gameObject.Name);
        }

        public void Remove(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
                Remove(gameObject);
        }

        public void ForEachEntity(Action<GameObject> action)
        {
            entities.ForEach(action);
        }
        public void ForEachEntity(Func<GameObject, bool> predicate, Action<GameObject> action)
        {
            entities.ForEach(predicate, action);
        }
        public void ForEachActiveEntity(Action<GameObject> action)
        {
            ForEachEntity(o => o.IsActive, action);
        }
        public void ForEachVisibleEntity(Action<GameObject> action)
        {
            ForEachEntity(o => o.IsVisible, action);
        }

        internal void LoadScene(Action<Scene> callback = null)
        {
            DebugConsole.Log("Loading Scene[" + Name + "]...");

            Load(() =>
            {
                IsActive = true;
                IsPaused = false;
                if (callback != null) callback(this);
            });
        }

        protected abstract void Load(Action done);

        internal void PauseScene()
        {
            IsPaused = true;
        }

        internal void UnpauseScene()
        {
            IsPaused = false;
        }

        /// <summary>
        /// Updates the Entities and the Scene itself.
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateScene(GameTime gameTime)
        {
            ForEachActiveEntity((entity) => entity.Update(gameTime));

            Update(gameTime);
        }

        public abstract void Update(GameTime gameTime);

        public void Draw(GameTime gameTime)
        {
            ForEachVisibleEntity((entity) => entity.Draw(gameTime));
        }

        public void Unload()
        {
            DebugConsole.Log("Unloading Scene[" + Name + "]...");

            var entityNames = Entities.Select(e => e.Name);
            foreach (var e in entityNames)
            {
                entities[e].IsActive = false;
                entities[e].IsVisible = false;
            }

            entities.Clear();
            IsPaused = false;
            IsActive = false;
        }

        #endregion
    }
}
