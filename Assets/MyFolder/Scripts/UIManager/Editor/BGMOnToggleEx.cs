using UnityEditor;
using UnityEditor.UI;

namespace Unity1Week.UI
{
    [CanEditMultipleObjects, CustomEditor(typeof(BGMOnToggle), true)]
    public class BGMOnToggleEx : ToggleEditor
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