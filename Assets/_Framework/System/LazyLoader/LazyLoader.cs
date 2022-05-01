using System.Collections.Generic;

namespace Game.System.LazyLoader
{
    public abstract class LazyLoader<T>
    {
        protected Dictionary<int, T> loadedItems;

        public LazyLoader()
        {
            loadedItems = new Dictionary<int, T>();
        }

        public T GetItem(int id)
        {
            T item;

            if (!loadedItems.TryGetValue(id, out item))
            {
                item = LoadItem(id);

                if (item != null)
                    loadedItems.Add(id, item);
            }

            return item;
        }

        protected abstract T LoadItem(int id);
    }
}