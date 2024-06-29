using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    private List<Tab> tabs;
    [SerializeField] Sprite tabIdle;
    [SerializeField] Sprite tabHover;
    [SerializeField] Sprite tabSelected;

    [SerializeField] private List<GameObject> UIpages;
    private Tab selectedTab;
    public void Subscribe(Tab tab)
    {
        if (tabs == null) tabs = new();

        tabs.Add(tab);

        ResetTabs();
    }


    public void OnTabEnter(Tab tab)
    {
        ResetTabs();

        if(selectedTab == null || tab != selectedTab)
        tab.SetBackground(tabHover);
    }
    public void OnTabExit(Tab tab)
    {
        ResetTabs();
    }
    public void OnTabSelected(Tab tab)
    {

        if (selectedTab != null) selectedTab.Deselect();

        selectedTab = tab;
        selectedTab.Select();

        ResetTabs();

        tab.SetBackground(tabSelected);

        int index = tab.transform.GetSiblingIndex();

        for(int i = 0; i <  UIpages.Count; i++)
        {
            if (i == index) UIpages[i].SetActive(true);
            else UIpages[i].SetActive(false);
        }
    }

    private void ResetTabs()
    {
        foreach(Tab tab in tabs)
        {
            if (tab == selectedTab) continue;

            tab.SetBackground(tabIdle);
        }
    }
}
