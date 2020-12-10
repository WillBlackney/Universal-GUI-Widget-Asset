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




        // Scaling Properties + Values
        private bool originalScaleIsSet = false;
        private Vector3 originalScale;

        [Header("General Transform Settings")]
        [ShowIf("ShowTransformToScale")]
        public Transform transformToScale;

        [Header("Enlarge Settings")]
        [ShowIf("ShowPercentageSizeIncrease")]
        [Range(1, 5f)]
        public float percentageSizeIncrease = 1f;

        [ShowIf("ShowEnlargeSpeed")]
        public float enlargeSpeed = 0.5f;

        [Header("Shrink Settings")]
        [ShowIf("ShowPercentageSizeDecrease")]
        [Range(0, 1f)]
        public float percentageSizeDecrease = 1f;

        [ShowIf("ShowShrinkSpeed")]
        public float shrinkSpeed;



        // Wiggle Properties
        private bool originalPositionIsSet = false;
        private Vector3 originalPosition;

        [Header("Wiggle Settings")]
        [ShowIf("ShowWiggleType")]
        public WiggleType wiggleType;

        [ShowIf("ShowWiggleType")]
        public Transform transformToWiggle;

        [ShowIf("ShowWiggleDistance")]
        public float wiggleDistance;

        [ShowIf("ShowWiggleType")]
        public float wiggleSpeed;

        [ShowIf("ShowWiggleType")]
        public bool wiggleInfinetly = false;

        [ShowIf("ShowNumberOfWiggles")]
        public int wiggleLoops;        

        [ShowIf("ShowRotationDegrees")]
        public float rotationDegrees;      

        #endregion

        // Accessors + Getters
        #region
        public bool OriginalScaleIsSet
        {
            get { return originalScaleIsSet; }
            private set { originalScaleIsSet = value; }
        }
        public Vector3 OriginalScale
        {
            get { return originalScale; }
            private set { originalScale = value; }
        }
        public bool OriginalPositionIsSet
        {
            get { return originalPositionIsSet; }
            private set { originalPositionIsSet = value; }
        }
        public Vector3 OriginalPosition
        {
            get { return originalPosition; }
            private set { originalPosition = value; }
        }
        #endregion

        // Misc Logic
        #region
        public void SetOriginalScale(Vector3 originalScale)
        {
            OriginalScale = originalScale;
            OriginalScaleIsSet = true;
        }
        public void SetOriginalPosition(Vector3 originalPosition)
        {
            OriginalPosition = originalPosition;
            OriginalPositionIsSet = true;
        }
        #endregion

        // Conditional Inspector View Logic
        #region
        public bool ShowWiggleDistance()
        {
            return widgetEventType == WidgetEvent.Wiggle &&
                (wiggleType == WiggleType.SideToSide || wiggleType == WiggleType.UpAndDown);
        }
        public bool ShowWiggleType()
        {
            return widgetEventType == WidgetEvent.Wiggle;
        }
        public bool ShowNumberOfWiggles()
        {
            return widgetEventType == WidgetEvent.Wiggle &&
                wiggleInfinetly == false;
        }
        public bool ShowRotationDegrees()
        {
            return widgetEventType == WidgetEvent.Wiggle &&
                wiggleType == WiggleType.RotateOnTheSpot;
        }
        public bool ShowEnlargeSpeed()
        {
            return widgetEventType == WidgetEvent.Enlarge ||
                widgetEventType == WidgetEvent.EnlargeAndShrink;
        }
        public bool ShowShrinkSpeed()
        {
            return widgetEventType == WidgetEvent.Shrink ||
                widgetEventType == WidgetEvent.EnlargeAndShrink;
        }
        public bool ShowTransformToScale()
        {
            return widgetEventType == WidgetEvent.Enlarge ||
                widgetEventType == WidgetEvent.Shrink ||
                widgetEventType == WidgetEvent.EnlargeAndShrink;
        }
        public bool ShowPercentageSizeIncrease()
        {
            return widgetEventType == WidgetEvent.Enlarge || widgetEventType == WidgetEvent.EnlargeAndShrink;
        }
        public bool ShowPercentageSizeDecrease()
        {
            return widgetEventType == WidgetEvent.Shrink || widgetEventType == WidgetEvent.EnlargeAndShrink;
        }
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
        Enlarge = 11,
        Shrink = 12,
        EnlargeAndShrink =13,
        Wiggle = 14,
    }
    public enum WidgetInputType
    {
        IPointer = 0,
        Collider = 1,
    }
    public enum WiggleType
    {
        RotateOnTheSpot = 0,
        SideToSide = 1,
        UpAndDown = 2,
    }
}
