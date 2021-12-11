using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [Header("The Tab to be selected on start")]

    public TabButton firstTabToSelect;
    bool firstOneIsSelected = false;

    [Header("===== Tabs ===== ")]

    public List<TabButton> tabButtons;

    [Header("===== Sprits ===== ")]

    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;

    [HideInInspector]
    public TabButton selectedTab;

    [Header("===== Pages ===== ")]

    public List<GameObject> pages;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();

            tabButtons.Add(button);
        }
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = button;

        selectedTab.Select();

        ResetTabs();
        button.background.sprite = tabActive;

        int index = button.transform.GetSiblingIndex();

        for (int i = 0; i < pages.Count; i++)
        {
            if (i == index)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) { continue; }
            button.background.sprite = tabIdle;
        }
    }

    public void firstTabLock()
    {
        firstOneIsSelected = false;
    }

    void Update()
    {
        if (gameObject.activeInHierarchy && firstOneIsSelected == false)
        {
            selectedTab = firstTabToSelect;
            selectedTab.Select();
            firstTabToSelect.background.sprite = tabActive;
            firstOneIsSelected = true;
        }
    }
}
