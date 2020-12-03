using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace BlackneyStudios.GuiWidget
{
    public class WidgetController : Singleton<WidgetController>
    {
        public void HandleWidgetEvents(Widget widget, WidgetEventData[] wEvents)
        {
            for (int i = 0; i < wEvents.Length; i++)
            {
                StartCoroutine(HandleWidgetEvent(widget, wEvents[i]));
            }
        }
        private IEnumerator HandleWidgetEvent(Widget widget, WidgetEventData wEvent)
        {
            // Wait for start delay
            if (wEvent.enableStartDelay)
            {
                yield return new WaitForSeconds(wEvent.startDelay);

                // Cancel if the pointer needs to be held over the
                // object, and the user has moved their mouse off the widget
                if (wEvent.onlyIfMouseIsStillOverMe &&
                    (widget.PointerIsOverMe == false || ((Time.realtimeSinceStartup - widget.TimeSinceLastPointerEnter) < wEvent.startDelay)))
                {
                    yield break;
                }
            }

            if (wEvent.widgetEventType == WidgetEvent.EnableGameObject)
            {
                wEvent.objectToEnable.SetActive(true);
            }
            else if (wEvent.widgetEventType == WidgetEvent.DisableGameObject)
            {
                wEvent.objectToDisable.SetActive(false);
            }
            else if (wEvent.widgetEventType == WidgetEvent.InvokeFunction)
            {
                wEvent.functionInvoked.Invoke();
            }
            else if (wEvent.widgetEventType == WidgetEvent.PlaySound)
            {
                AudioController.Instance.HandlePlayAudio(wEvent.audioSettings);
            }
            else if (wEvent.widgetEventType == WidgetEvent.FadeInCanvasGroup)
            {
                wEvent.canvasGroup.DOKill();
                wEvent.canvasGroup.alpha = 0;
                wEvent.canvasGroup.DOFade(1, wEvent.transistionSpeed);
            }
            else if (wEvent.widgetEventType == WidgetEvent.FadeOutCanvasGroup)
            {
                wEvent.canvasGroup.DOKill();
                wEvent.canvasGroup.alpha = 1;
                wEvent.canvasGroup.DOFade(0, wEvent.transistionSpeed);
            }
            else if (wEvent.widgetEventType == WidgetEvent.FadeInImage)
            {
                wEvent.image.DOKill();
                wEvent.image.DOFade(0, 0);
                wEvent.image.DOFade(1, wEvent.transistionSpeed);
            }
            else if (wEvent.widgetEventType == WidgetEvent.FadeOutImage)
            {
                wEvent.image.DOKill();
                wEvent.image.DOFade(1, 0);
                wEvent.image.DOFade(0, wEvent.transistionSpeed);
            }

            else if (wEvent.widgetEventType == WidgetEvent.TransisitionImageColour)
            {
                wEvent.image.DOKill();
                wEvent.image.DOColor(wEvent.endColour, wEvent.transistionSpeed);
            }

            else if (wEvent.widgetEventType == WidgetEvent.TransistionTextColour)
            {
                wEvent.text.DOKill();
                wEvent.text.DOColor(wEvent.endColour, wEvent.transistionSpeed);
            }
        }
    }

}

