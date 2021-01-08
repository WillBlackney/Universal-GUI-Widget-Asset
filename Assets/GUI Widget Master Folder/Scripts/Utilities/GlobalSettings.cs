using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BlackneyStudios.GuiWidget
{
    public class GlobalSettings : Singleton<GlobalSettings>
    {       
        // User Fields + Variables
        #region
        [Header("General Settings")]
        [Tooltip("Completely disables all widget events.")]
        [SerializeField] bool disableAllWidgetEvents = false;

        [Header("Logging Settings")]
        [Tooltip("Enables the printing of debug log statements from widgets to the console. " +
            "Disable this in game builds to improve performance.")]
        [SerializeField] bool enableDebugLogs = true;
        [Tooltip("Enables the printing of debug warning statements from widgets to the console. " +
            "Disable this in game builds to improve performance.")]
        [SerializeField] bool enableDebugWarnings = true;

        [Header("Device Settings")]
        [Tooltip("Set which type of input device your application targets.")]
        [SerializeField] InputDevice inputDevice = InputDevice.Mouse;
        #endregion

        // Properties + Accessors 
        #region
        public bool EnableDebugLogs() { return enableDebugLogs; }
        public bool EnableDebugWarnings() { return enableDebugWarnings; }
        #endregion
    }
}

