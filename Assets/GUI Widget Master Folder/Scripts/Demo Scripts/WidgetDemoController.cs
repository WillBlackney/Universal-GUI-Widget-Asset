using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace BlackneyStudios.GuiWidget
{
    public class WidgetDemoController : Singleton<WidgetController>
    {
        // Properties + Component References
        #region
        [SerializeField] GameObject menuBgParent;
        [SerializeField] GameObject menuButtonPanelParent;
        [SerializeField] Transform menuButtonPanelStartPos;
        [SerializeField] Transform menuButtonPanelEndPos;
        [SerializeField] Image blackScreenOverlayImage;
        [SerializeField] GameObject titleBannerParent;
        [SerializeField] Transform titleBannerStartPos;
        [SerializeField] Transform titleBannerEndPos;



        #endregion

        protected override void Awake()
        {
            // Set starting state of scene objects
            titleBannerParent.transform.DOMove(titleBannerStartPos.transform.position, 0);
            blackScreenOverlayImage.DOFade(1, 0);
            menuButtonPanelParent.transform.DOMove(menuButtonPanelStartPos.transform.position, 0);
            menuBgParent.transform.DOScale(1.2f, 0f);
            menuButtonPanelParent.SetActive(false);
        }

        IEnumerator Start()
        {
            // Do opening transistions + animations   
            blackScreenOverlayImage.DOFade(0, 1.5f);
            menuBgParent.transform.DOScale(1f, 1.5f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(1.5f);

            // Move title banner on screen
            titleBannerParent.SetActive(true);
            titleBannerParent.transform.DOMove(titleBannerEndPos.transform.position, 0.5f);
            yield return new WaitForSeconds(0.5f);

            // Move panel on screen
            menuButtonPanelParent.SetActive(true);
            menuButtonPanelParent.transform.DOMove(menuButtonPanelEndPos.transform.position, 0.5f);
        }
    }
}

