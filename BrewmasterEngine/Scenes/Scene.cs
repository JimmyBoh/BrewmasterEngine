using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Debug;
using BrewmasterEngine.Framework;
using Microsoft.Xna.Framework;

namespace BrewmasterEngine.Scenes
{
    public abstract class Scene
    {
        #region Constructors

        protected Scene(string name)
        {
            Name = name;
            entities = new Dictionary<string, GameObject>();
        }

        #endregion

        #region Properties

        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsPaused { get; private set; }

        private readonly Dictionary<string, GameObject> entities;
        public Dictionary<string, GameObject> EntityIndex { get { return entities; } }
        public GameObject[] Entities { get { return entities.Select(e => e.Value).ToArray(); } }

        #endregion

        #region Methods

        public void Add(GameObject gameObject)
        {
            entities[gameObject.Name] = gameObject;
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

        private void ForEach(IEnumerable<string> entityNames, Action<GameObject> action)
        {
            foreach (var e in entityNames.Where(e => entities.ContainsKey(e)))
                action(entities[e]);
        }
        public void ForEachEntity(Action<GameObject> action)
        {
            ForEach(entities.Keys, action);
        }
        public void ForEachActiveEntity(Action<GameObject> action)
        {
            ForEach(entities.Where(e => e.Value.IsActive).Select(e => e.Key).ToArray(), action);
        }
        public void ForEachVisibleEntity(Action<GameObject> action)
        {
            ForEach(entities.Where(e => e.Value.IsVisible).Select(e => e.Key).ToArray(), action);
        }
        public void ForEachNonpausableEntity(Action<GameObject> action)
        {
            ForEach(entities.Where(e => e.Value is INotPausable).Select(e => e.Key).ToArray(), action);
        }

        internal void LoadScene(Action<Scene> callback = null)
        {
            Debugger.Log("Loading Scene[" + Name + "]...");

            Load(() =>
            {
                IsActive = true;
                if (callback != null) callback(this);
            });
        }

        protected abstract void Load(Action done);

        internal void PauseScene()
        {
            IsPaused = true;
            ForEachNonpausableEntity((ent) =>
                {
                    var entity = ent as INotPausable;
                    if (entity != null) entity.OnPause();
                });
        }

        internal void UnpauseScene()
        {
            IsPaused = false;
            ForEachNonpausableEntity((ent) =>
            {
                var entity = ent as INotPausable;
                if (entity != null) entity.OnUnpause();
            });
        }

        public void Update(GameTime elapsedTime)
        {
            if (IsPaused)
                ForEachNonpausableEntity((entity) => entity.Update(elapsedTime));
            else
                ForEachActiveEntity((entity) => entity.Update(elapsedTime));
        }

        public void Draw(GameTime elapsedTime)
        {
            ForEachVisibleEntity((entity) => entity.Draw(elapsedTime));
        }

        public void Unload()
        {
            Debugger.Log("Unloading Scene[" + Name + "]...");

            var entityNames = Entities.Select(e => e.Name);
            foreach (var e in entityNames)
            {
                entities[e].IsActive = false;
                entities[e].IsVisible = false;
            }

            entities.Clear();
            IsActive = false;
        }

        #endregion
    }
}
