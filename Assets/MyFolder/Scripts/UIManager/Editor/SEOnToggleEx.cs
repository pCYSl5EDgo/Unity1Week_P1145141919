using UnityEditor;
using UnityEditor.UI;

namespace Unity1Week.UI
{
    [CanEditMultipleObjects, CustomEditor(typeof(SEOnToggle), true)]
    public class SEOnToggleEx : ToggleEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("titleSettings"), true);
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}