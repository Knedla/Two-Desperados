using System.Collections.Generic;
using UnityEngine;

namespace Game.System.Pooling
{
    public class PoolController
    {
        static Dictionary<int, PoolData> pools = new Dictionary<int, PoolData>();

        static GameObject pool;
        static GameObject Pool
        {
            get
            {
                if (pool == null)
                {
                    pool = new GameObject("Pool");
                    Object.DontDestroyOnLoad(pool);
                }

                return pool;
            }
        }

        static Poolable CreateInstance(int key, GameObject prefab)
        {
            Poolable poolable = Object.Instantiate(prefab).AddComponent<Poolable>();
            poolable.Key = key;

            return poolable;
        }

        public static void AddEntry<T>(int key, T prefab, int prepopulateAmount, int maxCount) where T : MonoBehaviour
        {
            if (pools.ContainsKey(key))
                return;

            PoolData poolData = new PoolData
            {
                Prefab = prefab.gameObject,
                MaxCount = maxCount,
                Pool = new Queue<Poolable>(prepopulateAmount)
            };

            pools.Add(key, poolData);

            for (int i = 0; i < prepopulateAmount; ++i)
                Enqueue(CreateInstance(key, prefab.gameObject));
        }

        public static void RemoveEntry(int key)
        {
            if (!pools.ContainsKey(key))
                return;

            PoolData poolData = pools[key];

            while (poolData.Pool.Count > 0)
            {
                Poolable poolable = poolData.Pool.Dequeue();

                if (poolable != null) //ovo je dodato kad se prekine igra nasilno da ne puca exception, ali u sustini nisam siguran da treba
                    Object.Destroy(poolable.gameObject);
            }

            pools.Remove(key);
        }

        public static void Enqueue(Poolable poolable)
        {
            if (poolable == null || poolable.IsPooled || !pools.ContainsKey(poolable.Key))
            {
                Object.Destroy(poolable.gameObject);
                return;
            }

            PoolData poolData = pools[poolable.Key];
            if (poolData.Pool.Count >= poolData.MaxCount)
            {
                poolable.transform.SetParent(Pool.transform);
                Object.Destroy(poolable.gameObject);
                return;
            }
            poolData.Pool.Enqueue(poolable);

            poolable.IsPooled = true;
            poolable.transform.SetParent(Pool.transform);
            poolable.gameObject.SetActive(false);
        }

        public static Poolable Dequeue(int key)
        {
            if (!pools.ContainsKey(key))
                return null;

            PoolData poolData = pools[key];

            if (poolData.Pool.Count == 0)
                return CreateInstance(key, poolData.Prefab);

            Poolable poolable = poolData.Pool.Dequeue();
            poolable.IsPooled = false;

            return poolable;
        }
    }
}
