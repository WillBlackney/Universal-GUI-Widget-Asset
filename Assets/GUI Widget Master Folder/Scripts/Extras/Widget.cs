using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BlackneyStudios.GUIWidget
{
    public class Widget : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        // Properties
        [SerializeField] private WidgetInputType inputType;

        [SerializeField] WidgetEventData[] mouseClickEvents;
        [SerializeField] WidgetEventData[] mouseEnterEvents;
        [SerializeField] WidgetEventData[] mouseExitEvents;
        public WidgetEventData[] MouseEnterEvents
        {
            get { return mouseEnterEvents; }
            private set { mouseEnterEvents = value; }
        }
        public WidgetEventData[] MouseClickEvents
        {
            get { return mouseClickEvents; }
            private set { mouseClickEvents = value; }
        }
        public WidgetEventData[] MouseExitEvents
        {
            get { return mouseExitEvents; }
            private set { mouseExitEvents = value; }
        }

        // IPointer Event Listeners
        public void OnPointerClick(PointerEventData eventData)
        {
            if (inputType == WidgetInputType.IPointer)
                WidgetController.Instance.HandleWidgetEventsAsBatch(MouseClickEvents);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (inputType == WidgetInputType.IPointer)
                WidgetController.Instance.HandleWidgetEventsAsBatch(MouseEnterEvents);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (inputType == WidgetInputType.IPointer)
                WidgetController.Instance.HandleWidgetEventsAsBatch(MouseExitEvents);
        }

        // Collider Input Listeners
        private void OnMouseDown()
        {
            if (inputType == WidgetInputType.Collider)
                WidgetController.Instance.HandleWidgetEventsAsBatch(MouseClickEvents);
        }
        private void OnMouseEnter()
        {
            if (inputType == WidgetInputType.Collider)
                WidgetController.Instance.HandleWidgetEventsAsBatch(MouseEnterEvents);
        }
        private void OnMouseExit()
        {
            if (inputType == WidgetInputType.Collider)
                WidgetController.Instance.HandleWidgetEventsAsBatch(MouseExitEvents);
        }
    }

    public enum WidgetInputType
    {
        IPointer = 0,
        Collider = 1,
        Both = 2,
    }
}

