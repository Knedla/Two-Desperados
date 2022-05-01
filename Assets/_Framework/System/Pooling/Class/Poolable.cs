using UnityEngine;

namespace Game.System.Pooling
{
    /// <summary>
    /// Used to map this object to a prefab in the PoolData class
    /// </summary>
    public class Poolable : MonoBehaviour
    {
        public int Key;
        public bool IsPooled;
    }
}
