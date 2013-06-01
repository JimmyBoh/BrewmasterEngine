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
            Entities = new GameObjectCollection();
        }

        #endregion

        #region Properties

        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsPaused { get; private set; }

        public GameObjectCollection Entities { get; private set; }

        #endregion

        #region Methods

        public void Add(GameObject gameObject)
        {
            Entities.Add(gameObject);
        }

        public void Add(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
                Add(gameObject);
        }

        public void Remove(string objectName)
        {
            Entities.Remove(objectName);
        }

        public void Remove(GameObject gameObject)
        {
            Remove(gameObject.Name);
        }

        public void Remove(IEnumerable<GameObject> gameObjects)
        {
            foreach (var name in gameObjects.Select(go => go.Name))
                Remove(name);
        }

        public void RemoveWhere(Func<GameObject, bool> predicate)
        {
            var names = Entities.Where(predicate).Select(e => e.Name).ToList();
            foreach (var name in names)
                Remove(name);
        }

        public T GetEntity<T>(Func<T, bool> predicate = null)
        {
            var entites = Entities.OfType<T>();
            return predicate != null ? entites.First(predicate) : entites.First();
        }

        public IEnumerable<T> GetEntities<T>(Func<T, bool> predicate = null)
        {
            var entites = Entities.OfType<T>();
            return predicate != null ? entites.Where(predicate) : entites;
        }

        public void ForEachEntity(Action<GameObject> action)
        {
            Entities.ForEach(action);
        }
        public void ForEachEntity(Func<GameObject, bool> predicate, Action<GameObject> action)
        {
            Entities.ForEach(predicate, action);
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
        /// Updates the the Scene itself. Entities should also be updated here.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            ForEachActiveEntity((entity) => entity.Update(gameTime));
        }

        public virtual void Draw(GameTime gameTime)
        {
            ForEachVisibleEntity((entity) => entity.Draw(gameTime));
        }

        public void Unload()
        {
            DebugConsole.Log("Unloading Scene[" + Name + "]...");

            ForEachEntity((e) => e is IDisposable, (e) => (e as IDisposable).Dispose());

            Entities.Clear();
            IsPaused = false;
            IsActive = false;
        }

        #endregion
    }
}
