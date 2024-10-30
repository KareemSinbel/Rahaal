using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SettingsTabsHandler : MonoBehaviour
{
    public List<GameObject> contentPanels;
    public List<UiTabButton> tabButtons;
    public Color32 tabIdleColor;
    public Color32 tabActiveColor;
    public Color32 tabHoverColor;

    public RectTransform settingsRect;
    public float initialPosX, targetPosX;
    public float tweenDuration;

    private int currentTabIndex;
    private UiTabButton selectedTabButton;


    private void Start()
    {
        OnTabSelected(tabButtons[0]);
    }

    public void Subscribe(UiTabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<UiTabButton>();
        }

        if(!tabButtons.Any(x=> x == button)) 
        {
            tabButtons.Add(button);
        }
    }


    public void OnTabEnter(UiTabButton button)
    {
        ResetTabs();
        if(selectedTabButton == null|| button != selectedTabButton) 
        {
            button.targetImage.color = tabHoverColor;
        }
    }

    public void OnTabExit(UiTabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(UiTabButton button)
    {
        selectedTabButton = button;
        ResetTabs();
        button.targetImage.color = tabActiveColor;

        ShowTabContent(tabButtons.FindIndex(x=> x == button));
    }


    public void ResetTabs()
    {
        foreach(var tab in tabButtons) 
        {
            if(selectedTabButton != null && selectedTabButton == tab)
            {
                continue;
            }
            tab.targetImage.color = tabIdleColor;
        }
    }

    public void ShowTabContent(int tabIndex)
    {
        for (int i = 0; i < contentPanels.Count; i++)
        {
            contentPanels[i].SetActive(i == tabIndex);
        }

        currentTabIndex = tabIndex;
    }

    public void MoveTabRight()
    {
        int index = (currentTabIndex + 1) % tabButtons.Count;
        OnTabSelected(tabButtons[index]);
    }

    public void MoveTabLeft()
    {
        int index = ((currentTabIndex - 1) + tabButtons.Count) % tabButtons.Count;
        OnTabSelected(tabButtons[index]);
    }

    private void SettingsPanelOutro()
    {
        settingsRect.DOAnchorPosX(initialPosX, tweenDuration).SetEase(Ease.InOutExpo).SetUpdate(true);
    }
    public void SettingsPanelIntro()
    {
        settingsRect.DOAnchorPosX(targetPosX, tweenDuration).SetEase(Ease.InOutQuart);
    }

}
