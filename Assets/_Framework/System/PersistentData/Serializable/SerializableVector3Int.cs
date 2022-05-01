using UnityEngine;
using System;

namespace Game.System.PersistentData
{
    [Serializable]
    public struct SerializableVector3Int
    {
        public int x;
        public int y;
        public int z;

        public SerializableVector3Int(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static implicit operator Vector3Int(SerializableVector3Int rValue)
        {
            return new Vector3Int(rValue.x, rValue.y, rValue.z);
        }

        public static implicit operator SerializableVector3Int(Vector3Int rValue)
        {
            return new SerializableVector3Int(rValue.x, rValue.y, rValue.z);
        }
    }
}