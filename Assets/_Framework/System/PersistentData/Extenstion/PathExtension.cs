using System.IO;
using UnityEngine;

namespace Game.System.PersistentData
{
    public static class PathExtension
    {
        public static string GetPersistentDataFullPath(this string relativePath)
        {
            return Path.Combine(Application.persistentDataPath, relativePath);
        }

        public static string GetPersistentDataFullPath(this string name, string relativePath)
        {
            return Path.Combine(Application.persistentDataPath, Path.Combine(relativePath, name));
        }

        public static string GetStreamingAssetsFullPath(this string name, string relativePath)
        {
            return Path.Combine(Application.streamingAssetsPath, Path.Combine(relativePath, name));
        }

        public static string GetStreamingAssetsFullPath(this string name, string relativePath1, string relativePath2)
        {
            return Path.Combine(Application.streamingAssetsPath, Path.Combine(Path.Combine(relativePath1, relativePath2), name));
        }
    }
}