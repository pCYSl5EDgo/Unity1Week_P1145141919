using UnityEditor;
using UnityEditor.UI;

namespace Unity1Week.UI
{
    [CanEditMultipleObjects, CustomEditor(typeof(TweetButton), true)]
    public sealed class TweetButtonEx : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("result"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("titleSettings"), true);
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}