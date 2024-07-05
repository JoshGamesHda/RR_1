using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatDisplay : MonoBehaviour
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
    public void ShowStatDisplay()
    {
        background.enabled = true;
        buildingImage.enabled = true;
        buildingName.enabled = true;
        buildingStats.enabled = true;
        buildingShape.enabled = true;
    }

    private void UpdateStats(Building b)
    {
        string text;
        if (!b.isSupport)
        {
            AttackTower a = (AttackTower) b;
            text = "Damage: " + a.damage.ToString() + "\nAttack Rate: " + a.fireRate.ToString() + "/s" + "\nRange: " + a.range.ToString();
            buildingStats.text = text;
        }
        else if(b.isSupport) 
        {
            SupportBuilding s = (SupportBuilding) b;
            //text = "Effect Strength: " + 
        }
    }
}
