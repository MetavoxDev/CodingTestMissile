/* Copyright 2018, Allan Arnaudin, All rights reserved. */
using UnityEditor;

namespace CustomKit
{
    [CustomEditor(typeof(CustomScriptableObject), true)]
    public class CustomScriptableObjectEditor : Editor
    {
        CustomScriptableObject editorTarget;
        SerializedObject serializedTarget;
        protected void OnEnable()
        {
            editorTarget = target as CustomScriptableObject;
            serializedTarget = new SerializedObject(editorTarget);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            SerializedProperty prop = serializedTarget.GetIterator();
            if (prop.NextVisible(true))
            {
                do
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(prop.name), true);
                }
                while (prop.NextVisible(false));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}
