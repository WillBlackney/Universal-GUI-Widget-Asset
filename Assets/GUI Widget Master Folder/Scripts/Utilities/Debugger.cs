using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BlackneyStudios.GuiWidget
{
    public static class Debugger
    {
        // NOTE: This class simple wraps Unity's Debug class.
        public static void Log(string message)
        {
            if (GlobalSettings.Instance &&
                GlobalSettings.Instance.EnableDebugLogs())
                Debug.Log(message);
        }
        public static void LogWarning(string message)
        {
            if (GlobalSettings.Instance &&
                GlobalSettings.Instance.EnableDebugWarnings())
                Debug.LogWarning(message);
        }
    }
}

