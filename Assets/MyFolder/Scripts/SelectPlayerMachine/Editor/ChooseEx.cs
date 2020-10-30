using UnityEditor;
using UnityEditor.UI;

namespace Unity1Week.SelectPlayer
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ChoosePlayerButton), true)]
    public class ChooseEx : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("displayText"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Kind"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("titleSettings"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}