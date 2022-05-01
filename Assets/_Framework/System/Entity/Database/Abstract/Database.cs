using Game.System.Entity.Definition;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.System.Entity.Database
{
    public abstract class Database<T> : MonoBehaviour where T : IDefinition
    {
        public static Database<T> Instance { get; private set; }

        public List<T> Items;

        Dictionary<Type, T> items;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                items = new Dictionary<Type, T>();

                foreach (T item in Items)
                    items.Add(item.Type, item);
            }
            else
                Destroy(gameObject);
        }

        public T GetDefinition<U>() where U : IDefinition
        {
            return items[typeof(U)];
        }
    }
}
