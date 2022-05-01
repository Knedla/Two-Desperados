using System.Collections.Generic;

namespace Game.System.LazyLoader
{
    public abstract class LazyLoaderWithPool<T, U> where T : class, new()
    {
        protected T pool;
        protected Dictionary<int, U> loadedItems;

        public LazyLoaderWithPool()
        {
            pool = new T();
            loadedItems = new Dictionary<int, U>();
        }

        public U GetItem(int id)
        {
            U item;

            if (!loadedItems.TryGetValue(id, out item))
            {
                item = LoadItem(id);

                if (item != null)
                    loadedItems.Add(id, item);
            }

            return item;
        }

        protected abstract U LoadItem(int id);
    }
}