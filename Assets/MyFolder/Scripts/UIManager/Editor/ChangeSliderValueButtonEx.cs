using UnityEditor;
using UnityEditor.UI;

namespace Unity1Week.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ChangeSliderValueButton), true)]
    public sealed class ChangeSliderValueButtonEx : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("slider"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("changeValue"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}