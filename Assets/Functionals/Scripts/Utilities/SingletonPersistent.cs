/* Copyright 2020, Allan Arnaudin, All rights reserved. */
namespace CustomKit
{
    public class SingletonPersistent<T> : Singleton<T> where T : SingletonPersistent<T>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyAtLoading();
        }

        protected virtual void DontDestroyAtLoading()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}