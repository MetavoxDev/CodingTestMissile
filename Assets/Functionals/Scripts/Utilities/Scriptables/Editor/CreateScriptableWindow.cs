/* Copyright 2018, Allan Arnaudin, All rights reserved. */
using System;
using UnityEditor;
using UnityEngine;

namespace Tool
{
    public class CreateScriptableWindow : EditorWindow
    {

        [MenuItem("Assets/Create/Scriptable", false, 30)]
        public static void OpenWindow()
        {
            var window = CreateInstance<CreateScriptableWindow>();
            window.Show();
        }

        private string filter = "";
        private string upperFilter;
        private Vector2 scrollPos;

        public void OnGUI()
        {
            GUI.SetNextControlName("FilterControl");
            filter = EditorGUILayout.TextField("SearchBar", filter);
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            bool doFilter = !string.IsNullOrEmpty(filter);

            if (doFilter)
            {
                upperFilter = filter.ToUpper();
            }
            bool any = false;
            Type lastType = null;


            int typeCount = 0;
            foreach (Type scriptableType in ScriptableUtility.ScriptableTypes)
            {
                if (doFilter && !scriptableType.Name.ToUpper().Contains(upperFilter)) continue;
                any = true;
                lastType = scriptableType;
                ++typeCount;
                if (GUILayout.Button(new GUIContent(scriptableType.Name)))
                {
                    Create(scriptableType);
                }
            }
            if (!any)
            {
                EditorGUILayout.HelpBox(
                    "No scriptable type containing " + filter,
                    MessageType.Warning);
            }
            EditorGUILayout.EndScrollView();
            EditorGUI.FocusTextInControl("FilterControl");

            var current = Event.current;
            if (current != null && current.isKey)
            {
                var key = current.keyCode;
                if (key == KeyCode.Escape)
                    Close();
                else if (key == KeyCode.KeypadEnter || key == KeyCode.Return && typeCount == 1)
                {
                    Create(lastType);
                }
            }
        }

        private void Create(Type scriptableType)
        {
            ScriptableUtility.CreateAsset(scriptableType);
            this.Close();
        }
    }
}
