using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    public RectTransform menuRect;
    public float initialMenuXPos, targetMenuXPos;

    public RectTransform logoRect;
    public float initialLogoYPos, targetLogoYPos;

    public float tweenDuration;

    public SettingsTabsHandler SettingsMenu { get; set; }

    private async Task OutroAnimation()
    {
        var tweenSequence = DOTween.Sequence();
        tweenSequence.Append(menuRect.DOAnchorPosX(targetMenuXPos, tweenDuration)).SetUpdate(true);
        await tweenSequence.Join(logoRect.DOAnchorPosY(targetLogoYPos, tweenDuration)).SetUpdate(true).AsyncWaitForCompletion();
    }

    private void IntroAnimation()
    {
        menuRect.DOAnchorPosX(initialMenuXPos, tweenDuration);
        logoRect.DOAnchorPosY(initialLogoYPos, tweenDuration);
    }


    public async void OpenSettings()
    {
        await OutroAnimation();

        var mainMenuCanvasGroup = gameObject.GetComponent<CanvasGroup>();
        mainMenuCanvasGroup.alpha = 0;
        mainMenuCanvasGroup.interactable = false;
        mainMenuCanvasGroup.blocksRaycasts = false;

        var settingsMenuCanvasGroup = SettingsMenu.gameObject.GetComponent<CanvasGroup>();
        settingsMenuCanvasGroup.alpha = 1;
        settingsMenuCanvasGroup.interactable = true;
        settingsMenuCanvasGroup.blocksRaycasts = true;

        SettingsMenu.SettingsPanelIntro();
    }
}
