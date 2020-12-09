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
            else if (wEvent.widgetEventType == WidgetEvent.Enlarge)
            {
                // Kill off any active animations on the transform
                wEvent.transformToScale.DOKill();

                // Calculate enlargement scale and convert it to to a vector 3
                Vector3 endScale = new Vector3(wEvent.OriginalScale.x * wEvent.percentageSizeIncrease,
                    wEvent.OriginalScale.y * wEvent.percentageSizeIncrease,
                    wEvent.OriginalScale.z * wEvent.percentageSizeIncrease);

                // Scale the transform to its new size
                wEvent.transformToScale.DOScale(endScale, wEvent.enlargeSpeed);
            }
            else if (wEvent.widgetEventType == WidgetEvent.Shrink)
            {
                // Kill off any active animations on the transform
                wEvent.transformToScale.DOKill();

                Vector3 endScale = new Vector3(wEvent.OriginalScale.x * wEvent.percentageSizeDecrease,
                    wEvent.OriginalScale.y * wEvent.percentageSizeDecrease,
                    wEvent.OriginalScale.z * wEvent.percentageSizeDecrease);

                // Scale the transform to its new size
                wEvent.transformToScale.DOScale(endScale, wEvent.shrinkSpeed);
            }
            else if (wEvent.widgetEventType == WidgetEvent.EnlargeAndShrink)
            {
                // Kill off any active animations on the transform
                wEvent.transformToScale.DOKill();

                // Calculate enlargement scale and convert it to to a vector 3
                Vector3 enlargeScale = new Vector3(wEvent.OriginalScale.x * wEvent.percentageSizeIncrease,
                    wEvent.OriginalScale.y * wEvent.percentageSizeIncrease,
                    wEvent.OriginalScale.z * wEvent.percentageSizeIncrease);

                // Calculate shrink scale and convert it to to a vector 3
                Vector3 decreaseScale = new Vector3(wEvent.OriginalScale.x * wEvent.percentageSizeDecrease,
                    wEvent.OriginalScale.y * wEvent.percentageSizeDecrease,
                    wEvent.OriginalScale.z * wEvent.percentageSizeDecrease);

                // Create an animation sequence chain
                Sequence s = DOTween.Sequence();

                // Add the enlargment animation to the sequence, then play it
                s.Append(wEvent.transformToScale.DOScale(enlargeScale, wEvent.enlargeSpeed));

                // Once the enlargment sequence is complete, play the shrink animation
                s.OnComplete(() => wEvent.transformToScale.DOScale(decreaseScale, wEvent.shrinkSpeed));                
            }
            else if (wEvent.widgetEventType == WidgetEvent.Wiggle &&
                wEvent.wiggleType == WiggleType.RotateOnTheSpot)
            {
                // Kill off any active animations on the transform
                wEvent.transformToScale.DOKill();

                WiggleSideToSide(wEvent);
            }
        }

        private void WiggleSideToSide(WidgetEventData wEvent)
        {
            Vector3 rightRotateVector = new Vector3(0, 0, wEvent.rotationDegrees);
            Vector3 leftRotateVector = new Vector3(0, 0, -wEvent.rotationDegrees);

            Sequence sequence = DOTween.Sequence(); // create a sequence
            sequence.Append(wEvent.transformToWiggle.DORotate(rightRotateVector, wEvent.wiggleSpeed / 2f)); 
            sequence.Append(wEvent.transformToWiggle.DORotate(leftRotateVector, wEvent.wiggleSpeed));
            sequence.Append(wEvent.transformToWiggle.DORotate(new Vector3(0,0,0), wEvent.wiggleSpeed));
            sequence.SetLoops(wEvent.numberOfWiggles); 
        }
    }

}

