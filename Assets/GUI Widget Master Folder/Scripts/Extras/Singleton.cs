using Sirenix.OdinInspector;
using UnityEngine;

namespace BlackneyStudios.GUIWidget
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        // NOTE: Any class that inherits from this will become a singleton
        // Core Properties
        #region
        public static T Instance;
        [SerializeField] bool dontDestroyOnLoad;
        #endregion

        // Singleton Creation Logic
        #region
        protected virtual void Awake()
        {
            BuildSingleton();
        }
        protected void BuildSingleton()
        {
            // Does the singleton already exist?
            if (!Instance)
            {
                // It doesn't exist yet, set this gameobject's generic script as the singleton instance
                Instance = GetComponent<T>();
                if (dontDestroyOnLoad)
                {
                    // Prevent destruction of the singleton on scene change, if marked to do so
                    DontDestroyOnLoad(Instance);
                }
            }
            else
            {
                // Singleton already exists, destroy self.
                Destroy(gameObject);
            }
        }

        #endregion
    }
}
