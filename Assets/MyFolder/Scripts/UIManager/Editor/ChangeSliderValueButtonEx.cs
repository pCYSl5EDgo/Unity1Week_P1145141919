using UnityEditor;
using UnityEditor.UI;

namespace Unity1Week.UI
{
    [CanEditMultipleObjects, CustomEditor(typeof(ChangeSliderValueButton), true)]
    public sealed class ChangeSliderValueButtonEx : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("slider"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("changeValue"), true);
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}