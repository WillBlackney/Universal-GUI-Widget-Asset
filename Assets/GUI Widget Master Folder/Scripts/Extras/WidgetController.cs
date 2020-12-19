using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace BlackneyStudios.GUIWidget
{
    public class WidgetController : Singleton<WidgetController>
    {
        // Core Logic for Handling Widget Events
        #region
        public void HandleWidgetEventsAsBatch(WidgetEventData[] wEvents)
        {            
            for (int i = 0; i < wEvents.Length; i++)
            {
                HandleWidgetEvent(wEvents[i]);
            }
        }
        private void HandleWidgetEvent(WidgetEventData wEvent)
        {
            // Enable Gameobject
            if (wEvent.widgetEventType == WidgetEvent.EnableGameObject)
            {
                wEvent.objectToEnable.SetActive(true);
            }

            // Disable Gameobject
            else if (wEvent.widgetEventType == WidgetEvent.DisableGameObject)
            {
                wEvent.objectToDisable.SetActive(false);
            }

            // Invoke Function
            else if (wEvent.widgetEventType == WidgetEvent.InvokeFunction)
            {
                wEvent.functionInvoked.Invoke();
            }

            // Modify Colour of Image Component
            else if (wEvent.widgetEventType == WidgetEvent.TransisitionImageColour)
            {
                wEvent.image.DOColor(wEvent.endColour, wEvent.transistionSpeed);
            }           
            
        }
        #endregion

    }
}


