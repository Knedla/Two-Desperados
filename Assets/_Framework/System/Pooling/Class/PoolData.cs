using System.Collections.Generic;
using UnityEngine;

namespace Game.System.Pooling
{
    public class PoolData
    {
        public GameObject Prefab { get; set; }
        public int MaxCount { get; set; }
        public Queue<Poolable> Pool { get; set; }
    }
}
