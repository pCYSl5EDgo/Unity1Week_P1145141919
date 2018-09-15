#if UNITY_EDITOR && UNITY_WEBGL
namespace Unity1Week
{
    using UnityEngine;
    using UnityEditor;

    [InitializeOnLoad]
    public class EditInitialSetting
    {
        static EditInitialSetting()
        {
            EditorApplication.update += Update;
        }

        static void Update()
        {
            bool isSuccess = EditorApplication.ExecuteMenuItem("Edit/Graphics Emulation/WebGL 2.0");
            if (isSuccess)
            {
                EditorApplication.update -= Update;
            }
        }
    }
}
#endif