using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BrewmasterEngine.DataTypes
{
    public class ObjectPool<T> : IEnumerable where T : IPoolable, new()
    {
        public ObjectPool(int capacity = 10)
        {
            deadObjects = new Queue<T>(capacity);
            activeObjects = new Queue<T>(capacity);
        }

        public int ActiveCount { get { return activeObjects.Count; } }
        public int InactiveCount { get { return deadObjects.Count; } }

        private readonly Queue<T> deadObjects;
        private readonly Queue<T> activeObjects;

        public T GetNew()
        {
            Update();

            if (deadObjects.Any())
            {
                var deadObj = deadObjects.Dequeue();
                deadObj.Reset();
                deadObj.IsFree = false;

                return deadObj;
            }

            var newObj = new T();
            newObj.Reset();

            return newObj;
        }

        public void Update()
        {
            ForEach((e) => e);
        }

        public void Add(T obj)
        {
            activeObjects.Enqueue(obj);
        }

        public void ForEach(Func<T, T> action)
        {
            var itemsLeft = activeObjects.Count;

            for (var i = itemsLeft; i > 0; i--)
            {
                var item = activeObjects.Dequeue();

                if (item.IsFree)
                    deadObjects.Enqueue(item);
                else
                    activeObjects.Enqueue(action(item));
            }
        }

        public void Clear()
        {
            ForEach((item) =>
            {
                item.IsFree = true;
                return item;
            });
        }

        public IEnumerator<T> GetEnumerator()
        {
            return activeObjects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}", activeObjects.Count, deadObjects.Count);
        }
    }
}
