using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Debugging;
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
            entities = new PriorityDictionary<string, GameObject, int>(o => o.ZIndex);
        }

        #endregion

        #region Properties

        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsPaused { get; private set; }

        private readonly PriorityDictionary<string, GameObject, int> entities;
        public Dictionary<string, GameObject> EntityIndex { get { return entities.ToDictionary(); } }
        public GameObject[] Entities { get { return entities.ToArray(); } }

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
        public void ForEachNonpausableEntity(Action<GameObject> action)
        {
            ForEachEntity(o => o is INotPausable, action);
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

        public virtual void Update(GameTime elapsedTime)
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
