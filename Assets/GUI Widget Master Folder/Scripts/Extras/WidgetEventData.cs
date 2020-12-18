using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BlackneyStudios.GUIWidget
{
    [System.Serializable]
    public class WidgetEventData
    {
        // Core Properties
        public WidgetEvent widgetEventType;
        public float transistionSpeed;

        // Enable + Disable Gameobject Properties
        public GameObject objectToDisable;
        public GameObject objectToEnable;

        // Function Invocation Properties
        public UnityEvent functionInvoked;

        // Manipulate Image Colour Properties
        public Image image;
        public Color endColour;
    }
    public enum WidgetEvent
    {
        None = 0,
        EnableGameObject = 1,
        DisableGameObject = 2,
        InvokeFunction = 3,
        TransisitionImageColour = 4,
    }
}

