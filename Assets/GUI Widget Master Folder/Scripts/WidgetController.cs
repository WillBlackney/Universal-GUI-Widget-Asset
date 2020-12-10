using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace BlackneyStudios.GuiWidget
{
    public class WidgetController : Singleton<WidgetController>
    {
        // Core Logic for Handling Widget Events
        #region
        public void HandleWidgetEvents(Widget widget, WidgetEventData[] wEvents)
        {
            // Stop + Kill any animations from previous events
            KillAllAnimationsOnWidget(widget);

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
                //wEvent.canvasGroup.DOKill();
                wEvent.canvasGroup.alpha = 0;
                wEvent.canvasGroup.DOFade(1, wEvent.transistionSpeed);
            }
            else if (wEvent.widgetEventType == WidgetEvent.FadeOutCanvasGroup)
            {
               // wEvent.canvasGroup.DOKill();
                wEvent.canvasGroup.alpha = 1;
                wEvent.canvasGroup.DOFade(0, wEvent.transistionSpeed);
            }
            else if (wEvent.widgetEventType == WidgetEvent.FadeInImage)
            {
                //wEvent.image.DOKill();
                wEvent.image.DOFade(0, 0);
                wEvent.image.DOFade(1, wEvent.transistionSpeed);
            }
            else if (wEvent.widgetEventType == WidgetEvent.FadeOutImage)
            {
               // wEvent.image.DOKill();
                wEvent.image.DOFade(1, 0);
                wEvent.image.DOFade(0, wEvent.transistionSpeed);
            }

            else if (wEvent.widgetEventType == WidgetEvent.TransisitionImageColour)
            {
                //wEvent.image.DOKill();
                wEvent.image.DOColor(wEvent.endColour, wEvent.transistionSpeed);
            }

            else if (wEvent.widgetEventType == WidgetEvent.TransistionTextColour)
            {
                //wEvent.text.DOKill();
                wEvent.text.DOColor(wEvent.endColour, wEvent.transistionSpeed);
            }
            else if (wEvent.widgetEventType == WidgetEvent.Enlarge)
            {
                // Kill off any active animations on the transform
                //wEvent.transformToScale.DOKill();

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
                //wEvent.transformToScale.DOKill();

                Vector3 endScale = new Vector3(wEvent.OriginalScale.x * wEvent.percentageSizeDecrease,
                    wEvent.OriginalScale.y * wEvent.percentageSizeDecrease,
                    wEvent.OriginalScale.z * wEvent.percentageSizeDecrease);

                // Scale the transform to its new size
                wEvent.transformToScale.DOScale(endScale, wEvent.shrinkSpeed);
            }
            else if (wEvent.widgetEventType == WidgetEvent.Breathe)
            {
                // Kill off any active animations on the transform
               // wEvent.transformToWiggle.DOKill();

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

                if(wEvent.scalingSequence == ScaleEventOrder.EnlargeThenShrink)
                {
                    // Add the enlargment animation to the sequence, then play it
                    s.Append(wEvent.transformToScale.DOScale(enlargeScale, wEvent.enlargeSpeed));

                    // Once the enlargment sequence is complete, play the shrink animation
                    s.OnComplete(() => wEvent.transformToScale.DOScale(decreaseScale, wEvent.shrinkSpeed));
                }
                else if (wEvent.scalingSequence == ScaleEventOrder.ShrinkThenEnlarge)
                {
                    // Add the enlargment animation to the sequence, then play it
                    s.Append(wEvent.transformToScale.DOScale(decreaseScale, wEvent.shrinkSpeed));

                    // Once the enlargment sequence is complete, play the shrink animation
                    s.OnComplete(() => wEvent.transformToScale.DOScale(enlargeScale, wEvent.enlargeSpeed));
                }


            }
            else if (wEvent.widgetEventType == WidgetEvent.Wiggle &&
                wEvent.wiggleType == WiggleType.RotateOnTheSpot)
            {
                WiggleOnTheSpot(wEvent);
            }

            else if (wEvent.widgetEventType == WidgetEvent.Wiggle &&
               wEvent.wiggleType == WiggleType.SideToSide)
            {
                WiggleSideToSide(wEvent);
            }
            else if (wEvent.widgetEventType == WidgetEvent.Wiggle &&
              wEvent.wiggleType == WiggleType.UpAndDown)
            {
                WiggleUpAndDown(wEvent);
            }
        }
        #endregion

        // Misc Logic
        #region
        private void KillAllAnimationsOnWidget(Widget widget)
        {
            for (int i = 0; i < widget.OnClickEvents.Length; i++)
            {
                // Kill transform scaling anims
                if(widget.OnClickEvents[i].transformToScale != null)
                {
                    widget.OnClickEvents[i].transformToScale.DOKill();
                }

                // Kill wiggle anims
                if (widget.OnClickEvents[i].transformToWiggle != null)
                {
                    widget.OnClickEvents[i].transformToWiggle.DOKill();
                }

                // Kill image anims
                if (widget.OnClickEvents[i].image != null)
                {
                    widget.OnClickEvents[i].image.DOKill();
                }

                // Kill cg anims
                if (widget.OnClickEvents[i].canvasGroup != null)
                {
                    widget.OnClickEvents[i].canvasGroup.DOKill();
                }

                // Kill text anims
                if (widget.OnClickEvents[i].text != null)
                {
                    widget.OnClickEvents[i].text.DOKill();
                }

            }
        }
        #endregion

        // Wiggle + Transform Events
        #region
        private void WiggleOnTheSpot(WidgetEventData wEvent)
        {
            Vector3 rightRotateVector = new Vector3(wEvent.OriginalRotation.x, wEvent.OriginalRotation.y,wEvent.OriginalRotation.z + wEvent.rotationDegrees);
            Vector3 leftRotateVector = new Vector3(wEvent.OriginalRotation.x, wEvent.OriginalRotation.y, wEvent.OriginalRotation.z - wEvent.rotationDegrees);

            int wiggleCount = wEvent.wiggleLoops +1;
            if (wEvent.wiggleInfinetly)
                wiggleCount = -1;

            Sequence s = DOTween.Sequence(); 
            s.Append(wEvent.transformToWiggle.DORotate(rightRotateVector, wEvent.wiggleSpeed / 2f)); 
            s.Append(wEvent.transformToWiggle.DORotate(leftRotateVector, wEvent.wiggleSpeed));
            s.Append(wEvent.transformToWiggle.DORotate(wEvent.OriginalRotation, wEvent.wiggleSpeed));
            s.SetLoops(wiggleCount); 
        }
        private void WiggleSideToSide(WidgetEventData wEvent)
        {
            int wiggleCount = wEvent.wiggleLoops +1;
            if (wEvent.wiggleInfinetly)
                wiggleCount = -1;

            // Move back to start pos
            wEvent.transformToWiggle.DOLocalMove(wEvent.OriginalPosition, 0f);

            // Start move
            Sequence s = DOTween.Sequence(); 
            s.Append(wEvent.transformToWiggle.DOLocalMoveX(wEvent.OriginalPosition.x + (wEvent.wiggleDistance / 2f), wEvent.wiggleSpeed / 2f));
            s.Append(wEvent.transformToWiggle.DOLocalMoveX(wEvent.OriginalPosition.x - wEvent.wiggleDistance, wEvent.wiggleSpeed));
            s.Append(wEvent.transformToWiggle.DOLocalMoveX(wEvent.OriginalPosition.x + (wEvent.wiggleDistance / 2f), wEvent.wiggleSpeed / 2f));
            s.SetLoops(wiggleCount);
        }

        private void WiggleUpAndDown(WidgetEventData wEvent)
        {
            int wiggleCount = wEvent.wiggleLoops + 1;
            if (wEvent.wiggleInfinetly)
                wiggleCount = -1;

            // Move back to start pos
            wEvent.transformToWiggle.DOLocalMove(wEvent.OriginalPosition, 0f);

            Sequence s = DOTween.Sequence();
            s.Append(wEvent.transformToWiggle.DOLocalMoveY(wEvent.OriginalPosition.y + (wEvent.wiggleDistance / 2f), wEvent.wiggleSpeed / 2f));
            s.Append(wEvent.transformToWiggle.DOLocalMoveY(wEvent.OriginalPosition.y - wEvent.wiggleDistance, wEvent.wiggleSpeed));
            s.Append(wEvent.transformToWiggle.DOLocalMoveY(wEvent.OriginalPosition.y + (wEvent.wiggleDistance / 2f), wEvent.wiggleSpeed / 2f));
            s.SetLoops(wiggleCount);
        }
        #endregion
    }

}

