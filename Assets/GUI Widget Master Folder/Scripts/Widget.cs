using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BlackneyStudios.GuiWidget
{
    public class Widget : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        // Variables + Component References
        #region
        [Header("Core Properties")]
        public WidgetInputType inputType;
        [PropertySpace(SpaceBefore = 20, SpaceAfter = 0)]

        [Header("Event Data")]
        [SerializeField] WidgetEventData[] onClickEvents;
        [PropertySpace(SpaceBefore = 20, SpaceAfter = 0)]
        [SerializeField] WidgetEventData[] mouseEnterEvents;
        [PropertySpace(SpaceBefore = 20, SpaceAfter = 0)]
        [SerializeField] WidgetEventData[] mouseExitEvents;
        [PropertySpace(SpaceBefore = 20, SpaceAfter = 0)]

        [Header("Input State")]
        private bool pointerIsOverMe;
        private float timeSinceLastPointerEnter;
        [PropertySpace(SpaceBefore = 20, SpaceAfter = 0)]
        #endregion

        //  Properties + Accessors
        #region
        public bool PointerIsOverMe
        {
            get { return pointerIsOverMe; }
            private set { pointerIsOverMe = value; }
        }
        public float TimeSinceLastPointerEnter
        {
            get { return timeSinceLastPointerEnter; }
            private set { timeSinceLastPointerEnter = value; }
        }

        public WidgetEventData[] MouseEnterEvents
        {
            get { return mouseEnterEvents; }
            private set { mouseEnterEvents = value; }
        }
        public WidgetEventData[] OnClickEvents
        {
            get { return onClickEvents; }
            private set { onClickEvents = value; }
        }
        public WidgetEventData[] MouseExitEvents
        {
            get { return mouseExitEvents; }
            private set { mouseExitEvents = value; }
        }
        #endregion

        // Input Listeners
        #region
        public void OnPointerClick(PointerEventData eventData)
        {
            if (inputType == WidgetInputType.IPointer)
            {
                WidgetController.Instance.HandleWidgetEvents(this, OnClickEvents);
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (inputType == WidgetInputType.IPointer)
            {
                PointerIsOverMe = true;
                TimeSinceLastPointerEnter = Time.realtimeSinceStartup;
                WidgetController.Instance.HandleWidgetEvents(this, MouseEnterEvents);
            }

        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (inputType == WidgetInputType.IPointer)
            {
                PointerIsOverMe = false;
                WidgetController.Instance.HandleWidgetEvents(this, MouseExitEvents);
            }

        }

        public void OnMouseDown()
        {
            if (inputType == WidgetInputType.Collider)
            {
                WidgetController.Instance.HandleWidgetEvents(this, OnClickEvents);
            }
        }
        public void OnMouseEnter()
        {
            if (inputType == WidgetInputType.Collider)
            {
                PointerIsOverMe = true;
                TimeSinceLastPointerEnter = Time.realtimeSinceStartup;
                WidgetController.Instance.HandleWidgetEvents(this, MouseEnterEvents);
            }
        }
        public void OnMouseExit()
        {
            if (inputType == WidgetInputType.Collider)
            {
                PointerIsOverMe = false;
                WidgetController.Instance.HandleWidgetEvents(this, MouseExitEvents);
            }
        }      
        #endregion
    }
}

