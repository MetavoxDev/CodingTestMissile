/* Copyright 2020, Allan Arnaudin, All rights reserved. */
using UnityEngine;

namespace CustomKit
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        protected static T instance;

        public static T Instance
        {
            get { return instance; }
        }
        public static bool HasInstance
        {
            get { return (instance); }
        }
        protected virtual void Awake()
        {
            if (instance)
            {
                Debug.LogError("Already a Singleton ! " + typeof(T).ToString() + " in " + this.gameObject.name + " and i am in " + instance.gameObject.name);
                Destroy(this.gameObject);
                return;
            }

            instance = (T)this;
        }
    }
}