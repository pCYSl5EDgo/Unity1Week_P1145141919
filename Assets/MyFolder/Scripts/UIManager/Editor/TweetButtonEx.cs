using UnityEditor;
using UnityEditor.UI;

namespace Unity1Week.UI
{
    [CanEditMultipleObjects]
    public sealed class TweetButtonEx : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("result"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("titleSettings"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}