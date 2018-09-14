using UnityEditor;
using UnityEditor.UI;

namespace Unity1Week.UI
{
    [CanEditMultipleObjects, CustomEditor(typeof(DifficultyYSlider), true)]
    public class DifficultyYSliderEx : SliderEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("titleSettings"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("text"), true);
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}