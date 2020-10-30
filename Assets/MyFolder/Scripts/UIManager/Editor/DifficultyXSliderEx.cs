﻿using UnityEditor;
using UnityEditor.UI;

namespace Unity1Week.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(DifficultyXSlider), true)]
    public class DifficultyXSliderEx : SliderEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("titleSettings"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("text"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}