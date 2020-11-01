using UnityEditor;
using UnityEditor.UI;

namespace Unity1Week.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SEOnToggle), true)]
    public class SEOnToggleEx : ToggleEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("titleSettings"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}