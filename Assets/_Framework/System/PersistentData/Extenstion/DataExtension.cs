using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game.System.PersistentData
{
    public static class DataExtension
    {
        public static T LoadDataFromFile<T>(string path) where T : class
        {
            if (!File.Exists(path))
                return null;

            using (FileStream file = File.Open(path, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(file);
            }
        }

        public static void SaveDataToFile<T>(T arg, string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (FileStream file = File.Create(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, arg);
            }
        }
    }
}
