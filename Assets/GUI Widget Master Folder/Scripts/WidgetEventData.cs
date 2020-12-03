using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

namespace BlackneyStudios.GuiWidget
{   
    [Serializable]
    public class WidgetEventData
    {
        // Properties + References
        #region
        [Header("Core Event Properties")]
        public WidgetEvent widgetEventType;

        [ShowIf("ShowDelayProperties")]
        public bool enableStartDelay;

        [ShowIf("enableStartDelay", true)]
        public float startDelay;

        [ShowIf("enableStartDelay", true)]
        public bool onlyIfMouseIsStillOverMe;

        [ShowIf("widgetEventType", WidgetEvent.DisableGameObject)]
        public GameObject objectToDisable;

        [ShowIf("widgetEventType", WidgetEvent.EnableGameObject)]
        public GameObject objectToEnable;

        [ShowIf("widgetEventType", WidgetEvent.InvokeFunction)]
        public UnityEvent functionInvoked;

        [ShowIf("widgetEventType", WidgetEvent.PlaySound)]
        public AudioModel audioSettings;

        [ShowIf("ShowCanvasField")]
        public CanvasGroup canvasGroup;

        [ShowIf("ShowImageField")]
        public Image image;

        [ShowIf("widgetEventType", WidgetEvent.TransistionTextColour)]
        public TextMeshProUGUI text;

        [ShowIf("ShowEndColour")]
        public Color endColour;

        [ShowIf("ShowFadeSpeed")]
        public float transistionSpeed;
        #endregion

        // Conditional Inspector View Logic
        #region
        public bool ShowDelayProperties()
        {
            return widgetEventType == WidgetEvent.EnableGameObject ||
                widgetEventType == WidgetEvent.FadeInCanvasGroup ||
                widgetEventType == WidgetEvent.FadeInImage;
        }
        public bool ShowEndColour()
        {
            return widgetEventType == WidgetEvent.TransistionTextColour || widgetEventType == WidgetEvent.TransisitionImageColour;
        }
        public bool ShowCanvasField()
        {
            return widgetEventType == WidgetEvent.FadeInCanvasGroup || widgetEventType == WidgetEvent.FadeOutCanvasGroup;
        }
        public bool ShowImageField()
        {
            return widgetEventType == WidgetEvent.FadeInImage ||
                widgetEventType == WidgetEvent.FadeOutImage ||
                widgetEventType == WidgetEvent.TransisitionImageColour;
        }
        public bool ShowFadeSpeed()
        {
            return widgetEventType == WidgetEvent.FadeInCanvasGroup ||
                   widgetEventType == WidgetEvent.FadeOutCanvasGroup ||
                   widgetEventType == WidgetEvent.FadeInImage ||
                   widgetEventType == WidgetEvent.FadeOutImage ||
                   widgetEventType == WidgetEvent.TransisitionImageColour ||
                   widgetEventType == WidgetEvent.TransistionTextColour;
        }
        #endregion
    }
    public enum WidgetEvent
    {
        None = 0,
        EnableGameObject = 1,
        DisableGameObject = 2,
        InvokeFunction = 3,
        PlaySound = 4,
        FadeInCanvasGroup = 5,
        FadeOutCanvasGroup = 6,
        FadeInImage = 7,
        FadeOutImage = 8,
        TransisitionImageColour = 9,
        TransistionTextColour = 10,
    }
    public enum WidgetInputType
    {
        IPointer = 0,
        Collider = 1,
    }
}
