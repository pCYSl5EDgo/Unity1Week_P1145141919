using UnityEditor;
using UnityEditor.UI;

namespace Unity1Week.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AdditiveOpenButton), true)]
    public class AdditiveOpenButtonEx : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sceneIndex"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}