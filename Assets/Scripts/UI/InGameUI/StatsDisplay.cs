using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image buildingImage;
    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI buildingStats;
    [SerializeField] private Image buildingShape;

    public void UpdateStatDisplay(Building displayBuilding)
    {
        buildingImage.sprite = displayBuilding.buttonData.buildingImage;
        buildingName.text = displayBuilding.buttonData.buildingName;
        UpdateStats(displayBuilding);
        buildingShape.sprite = displayBuilding.buttonData.buildingShape;
    }

    public void HideStatDisplay()
    {
        background.enabled = false;
        buildingImage.enabled = false;
        buildingName.enabled = false;
        buildingStats.enabled = false;
        buildingShape.enabled = false;
    }
    public void ShowStatDiscplay()
    {
        background.enabled = true;
        buildingImage.enabled = true;
        buildingName.enabled = true;
        buildingStats.enabled = true;
        buildingShape.enabled = true;
    }

    private void UpdateStats(Building displayBuilding)
    {

    }
}
