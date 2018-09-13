using UnityEditor;
using UnityEditor.UI;

namespace Unity1Week.UI
{
    [CanEditMultipleObjects, CustomEditor(typeof(AdditiveOpenButton), true)]
    public class AdditiveOpenButtonEx : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("sceneIndex"), true);
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}