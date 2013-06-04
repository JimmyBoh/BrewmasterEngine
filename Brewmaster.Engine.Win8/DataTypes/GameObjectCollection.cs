using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BrewmasterEngine.DataTypes
{
    public class GameObjectCollection : IEnumerable<GameObject>
    {
        #region Constructor

        public GameObjectCollection()
        {
            gameObjects = new PriorityDictionary<string, GameObject, int>((obj) => obj.ZIndex);
        }

        #endregion

        #region Properties

        private readonly PriorityDictionary<string, GameObject, int> gameObjects;
        
        public GameObject this[string key]
        {
            get { return gameObjects[key]; }
            set { Add(value); }
        }

        #endregion

        #region Methods

        public void Add(GameObject entity)
        {
            gameObjects.Add(entity.Name, entity);
        }

        public void AddRange(IEnumerable<GameObject> entities)
        {
            gameObjects.AddRange(entities.Select(e => new KeyValuePair<string, GameObject>(e.Name, e)));
        }

        public void Remove(string name)
        {
            gameObjects.Remove(name);
        }

        public void ForEach(Action<GameObject> action)
        {
            ForEach(o => true, action);
        }

        public void ForEach(Func<GameObject, bool> predicate, Action<GameObject> action)
        {
            gameObjects.ForEach(predicate, (obj) =>
            {
                if (obj.IsFree)
                    gameObjects.Remove(obj.Name);
                else
                    action(obj);
            });
        }

        public void Clear()
        {
            gameObjects.Clear();
        }

        public IEnumerator<GameObject> GetEnumerator()
        {
            return gameObjects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
