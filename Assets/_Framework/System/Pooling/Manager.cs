using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.System.Pooling
{
    public class Manager : IManager
    {
        int key;
        Dictionary<string, int> keyMapper;

        public Manager()
        {
            key = 0;
            keyMapper = new Dictionary<string, int>();
        }

        string CreateMapperKey(GameObject parent, Type prefabType)
        {
            return string.Concat(parent.GetInstanceID(), prefabType);
        }

        public void Register(GameObject parent, MonoBehaviour prefab, int? prepopulateAmt = null, int? maxCount = null)
        {
            Register(parent, prefab.GetType(), prefab, prepopulateAmt, maxCount);
        }

        public void Register(GameObject parent, Type prefabType, MonoBehaviour prefab, int? prepopulateAmt = null, int? maxCount = null)
        {
            string mapperKey = CreateMapperKey(parent, prefabType);

            if (keyMapper.ContainsKey(mapperKey))
                return;

            key++;
            keyMapper.Add(mapperKey, key);

            PoolController.AddEntry(key, prefab, (prepopulateAmt == null) ? 0 : prepopulateAmt.Value, (maxCount == null) ? int.MaxValue : maxCount.Value);
        }

        public void Unregister<T>(GameObject parent) where T : MonoBehaviour
        {
            Unregister(parent, typeof(T));
        }

        public void Unregister(GameObject parent, Type prefabType)
        {
            string mapperKey = CreateMapperKey(parent, prefabType);

            int key;
            keyMapper.TryGetValue(mapperKey, out key);

            if (key == 0)
                return;

            keyMapper.Remove(mapperKey);

            PoolController.RemoveEntry(key);
        }

        public void Enqueue(Poolable item)
        {
            PoolController.Enqueue(item);
        }

        public void Enqueue(GameObject item)
        {
            Poolable poolable = item.GetComponent<Poolable>();

            if (poolable != null)
                Enqueue(poolable);
        }

        public void Enqueue(MonoBehaviour item)
        {
            Poolable poolable = item.GetComponent<Poolable>();

            if (poolable != null)
                Enqueue(poolable);
        }

        public void EnqueueAll(GameObject parent)
        {
            while (parent.transform.childCount > 0)
                Framework.PoolManager.Enqueue(parent.transform.GetChild(0).gameObject);
        }

        public T Dequeue<T>(GameObject parent) where T : MonoBehaviour
        {
            GameObject gameObject = Dequeue(parent, typeof(T));
            return (gameObject != null) ? gameObject.GetComponent<T>() : null;
        }

        public GameObject Dequeue(GameObject parent, Type prefabType)
        {
            int key;
            keyMapper.TryGetValue(CreateMapperKey(parent, prefabType), out key);

            if (key == 0)
                return null;

            Poolable poolable = PoolController.Dequeue(key);

            poolable.gameObject.transform.SetParent(parent.transform);
            poolable.gameObject.SetActive(true);

            return poolable.gameObject;
        }
    }
}
