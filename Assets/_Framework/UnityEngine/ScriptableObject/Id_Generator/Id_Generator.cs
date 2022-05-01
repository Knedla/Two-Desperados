using System.IO;
using UnityEditor;

namespace UnityEngine
{
    public class Id_Generator : ScriptableObject // for onemanband use
    {
        const string FilePath = "Assets/_Framework/UnityEngine/ScriptableObject/Id_Generator/Generated";
        const string FileNameWithExtension = "Id_Generator.asset";
        const string Label = "3688e26d-9d5e-43f1-b5af-a6a66ebd92a7";
        const string SearchForLabel = "l:" + Label;

        static Id_Generator instance;
        public static Id_Generator Instance
        {
            get
            {
                if (instance == null)
                {
                    string[] guids = AssetDatabase.FindAssets(SearchForLabel, new[] { FilePath });

                    if (guids.Length == 0)
                        CreateAsset();
                    else
                        instance = AssetDatabase.LoadAssetAtPath<Id_Generator>(AssetDatabase.GUIDToAssetPath(guids[0]));

                    EditorUtility.SetDirty(instance);
                }

                return instance;
            }
        }

        static void CreateAsset()
        {
            string projectBasePath = Path.Combine(Path.GetDirectoryName(Application.dataPath), FilePath);

            if (!Directory.Exists(projectBasePath))
                Directory.CreateDirectory(projectBasePath);

            instance = CreateInstance<Id_Generator>();

            AssetDatabase.CreateAsset(instance, Path.Combine(FilePath, FileNameWithExtension));
            AssetDatabase.SetLabels(instance, new string[] { Label });
        }

        public int LastAssignedId;

        public int GenerateId()
        {
            return ++LastAssignedId;
        }
    }
}
