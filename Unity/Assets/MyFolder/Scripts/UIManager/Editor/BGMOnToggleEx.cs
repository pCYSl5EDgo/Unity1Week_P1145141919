using UnityEditor;
using UnityEditor.UI;

namespace Unity1Week.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BGMOnToggle), true)]
    public class BGMOnToggleEx : ToggleEditor
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