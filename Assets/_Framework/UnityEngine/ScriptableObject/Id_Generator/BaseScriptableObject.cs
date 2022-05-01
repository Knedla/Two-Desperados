using UnityEditor;

namespace UnityEngine
{
    class ScriptableObjectIdAttribute : PropertyAttribute { }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ScriptableObjectIdAttribute))]
    class ScriptableObjectIdDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.intValue == 0)
                property.intValue = Id_Generator.Instance.GenerateId();

            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
#endif

    public abstract class BaseScriptableObject : ScriptableObject
    {
        [ScriptableObjectId]
        public int id;
        public int Id { get { return id; } }
    }
}
