using System;
using UnityEngine;

namespace Game.System.Pooling
{
    //pokusavam ga prebaciti da ne koristi <T>
    public interface IManager
    {
        void Register(GameObject parent, MonoBehaviour prefab, int? prepopulateAmt = null, int? maxCount = null);
        void Register(GameObject parent, Type prefabType, MonoBehaviour prefab, int? prepopulateAmt = null, int? maxCount = null);

        void Unregister<T>(GameObject parent) where T : MonoBehaviour;
        void Unregister(GameObject parent, Type prefabType);

        void Enqueue(GameObject item);
        void Enqueue(MonoBehaviour item);

        void EnqueueAll(GameObject parent);

        T Dequeue<T>(GameObject parent) where T : MonoBehaviour;
        GameObject Dequeue(GameObject parent, Type prefabType);
    }
}
