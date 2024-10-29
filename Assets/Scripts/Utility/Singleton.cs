using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Singleton
{
    /// <summary>
    /// this script inherits from MonoBehaviour
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;
        private static object _lock = new object();

        protected MonoSingleton() { }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance = FindObjectOfType<T>();
                        if (_instance != null)
                        {
                            GameObject go = new GameObject(typeof(T).Name);
                            _instance = go.AddComponent<T>();
                        }
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = (T)this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            _instance = null;
        }

        private void OnApplicationQuit()
        {
            _instance = null;
        }
    }

    /// <summary>
    /// this script don't inherit from MonoBehaviour
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NoMonoSingleton<T> where T : NoMonoSingleton<T>
    {
        private static T _instance;
        private static object _lock = new object();

        protected NoMonoSingleton() { }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= Activator.CreateInstance(typeof(T), true) as T;
                    }
                }
                return _instance;
            }
        }
    }
}


