﻿using Sirenix.OdinInspector;
using UnityEngine;


namespace BlackneyStudios.GuiWidget
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        // Any class that inherits from this will become a singleton

        // Properties
        #region
        public static T Instance;

        [Header("Singleton Properties")]
        [SerializeField] bool dontDestroyOnLoad;
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 20)]
        #endregion

        // Singleton Creation Logic
        #region
        protected virtual void Awake()
        {
            BuildSingleton();
        }
        protected void BuildSingleton()
        {
            if (!Instance)
            {
                Instance = GetComponent<T>();
                if (dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(Instance);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}

