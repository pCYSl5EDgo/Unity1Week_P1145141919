using UnityEditor;
using UnityEditor.UI;

namespace Unity1Week.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(閉じるボタン), true)]
    public sealed class CloseButtonEx : ButtonEditor
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