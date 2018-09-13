using UnityEditor;
using UnityEditor.UI;

namespace Unity1Week.SelectPlayer
{
    [CanEditMultipleObjects, CustomEditor(typeof(ChoosePlayerButton), true)]
    public class ChooseEx : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("displayText"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("Kind"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("titleSettings"), true);
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}