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

        [Header("Misc Properties")]
        private bool hasRunSetup = false;
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

        // Setup + Initialization
        #region
        void Start()
        {
            // Runs the setup as soon as the application is launched.
            // NOTE: 'Start' is only executed on game objects that are active, if the
            // game object is disabled when the application starts, the set up will not run.
            // To remedy this, whenever this game object is enabled, it will check if the set up
            // has already executed. If it hasn't, it will run the set up as part of the 'OnEnable' event.
            if (!hasRunSetup)
            {
                RunSetup();
            }
        }
        void OnEnable()
        {
            // If the set up was not executed during 'Start' (because this game object was disabled)
            // then run the setup on first enable.
            if (!hasRunSetup)
            {
                RunSetup();
            }
        }
        void RunSetup()
        {
            // Set and cache original scaling values of transforms for shrink/enlarge/etc events,
            // but only if the widget event manipulates a transform's scale is some way

            // Set up on click events
            for(int i = 0; i < onClickEvents.Length; i++)
            {
                if (OnClickEvents[i].transformToScale != null && !OnClickEvents[i].OriginalScaleIsSet)
                {
                    OnClickEvents[i].SetOriginalScale(OnClickEvents[i].transformToScale.localScale);
                }
                if (OnClickEvents[i].transformToWiggle != null && !OnClickEvents[i].OriginalPositionIsSet)
                {
                    OnClickEvents[i].SetOriginalPosition(OnClickEvents[i].transformToWiggle.localPosition);
                }
            }

            // Set up on mouse enter events
            for (int i = 0; i < MouseEnterEvents.Length; i++)
            {
                if (MouseEnterEvents[i].transformToScale != null && !MouseEnterEvents[i].OriginalScaleIsSet)
                {
                    MouseEnterEvents[i].SetOriginalScale(MouseEnterEvents[i].transformToScale.localScale);
                }
                if (MouseEnterEvents[i].transformToWiggle != null && !MouseEnterEvents[i].OriginalPositionIsSet)
                {
                    MouseEnterEvents[i].SetOriginalPosition(MouseEnterEvents[i].transformToWiggle.localPosition);
                }
            }

            // Set up on mouse exit events
            for (int i = 0; i < MouseExitEvents.Length; i++)
            {
                if (MouseExitEvents[i].transformToScale != null && !MouseExitEvents[i].OriginalScaleIsSet)
                {
                    MouseExitEvents[i].SetOriginalScale(MouseExitEvents[i].transformToScale.localScale);
                }
                if (MouseExitEvents[i].transformToWiggle != null && !MouseExitEvents[i].OriginalPositionIsSet)
                {
                    MouseExitEvents[i].SetOriginalPosition(MouseExitEvents[i].transformToWiggle.localPosition);
                }
            }

            hasRunSetup = true;
        }
        #endregion
    }
}

