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
            Debug.Log("UI click detected on " + this.gameObject.name);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        // Collider Input Listeners
        private void OnMouseDown()
        {
            Debug.Log("Collider click detected on " + this.gameObject.name);
        }
        private void OnMouseEnter()
        {

        }
        private void OnMouseExit()
        {

        }
    }

    public enum WidgetInputType
    {
        IPointer = 0,
        Collider = 1,
        Both = 2,
    }
}

