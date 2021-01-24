/* Copyright 2018, Allan Arnaudin, All rights reserved. */
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class ScriptableUtility
{
    public static void CreateAsset(Type type)
    {
        ScriptableObject asset = ScriptableObject.CreateInstance(type);

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(path), "");
        }

        AssetDatabase.CreateAsset(asset, path + "/" + type.Name +".asset");
        AssetDatabase.SaveAssets();
    }

    public static T CreateAssetAt<T>(string name, string path) where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();
        AssetDatabase.CreateAsset(asset, path + "/" + name + ".asset");
        AssetDatabase.SaveAssets();
        return asset;
    }

    public static void CreateAsset<T>(string name) where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(path), "");
        }
        AssetDatabase.CreateAsset(asset, path);
    }

    private static List<Type> _scriptableTypes;

    public static List<Type> ScriptableTypes
    {
        get
        {
            if (_scriptableTypes == null)
            {
                _scriptableTypes = new List<Type>();

                Type[] types = Assembly.GetExecutingAssembly().GetTypes();

                for (int i = 0; i < types.Length; i++)
                {
                    if (types[i].IsSubclassOf(typeof(CustomScriptableObject)) && !types[i].IsAbstract)
                    {
                        if(types[i].Namespace != "ToolUtility")
                        {
                            _scriptableTypes.Add(types[i]);
                        }
                    }
                }
                _scriptableTypes.Sort((t1, t2) => string.CompareOrdinal(t1.Name, t2.Name));
            }
            return _scriptableTypes;
        }
    }
}
#endif