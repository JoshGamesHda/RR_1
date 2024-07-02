using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject assignedBuilding { get; set; }

    private TextMeshProUGUI textComp;
    private void OnEnable()
    {
        textComp = GetComponentInChildren<TextMeshProUGUI>();
        gameObject.GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }
    private void Update()
    {
        if(assignedBuilding != null)
        textComp.text = assignedBuilding.GetComponent<Building>().buttonData.buildingName + "\n" + assignedBuilding.GetComponent<Building>().buttonData.buildingDescription;
    }
    private void OnButtonClick()
    {
        BuildingManager.Instance.SetActiveBuilding(BuildingPool.Instance.GetBuilding(assignedBuilding.GetComponent<Building>().identifier));
        BuildingPool.Instance.lastPickedBuilding = assignedBuilding.GetComponent<Building>().identifier;

        assignedBuilding = null;
        
        UIManager.Instance.ReturnBuildingsToPool();
    }
}