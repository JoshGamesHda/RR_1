using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private Sprite backgroundAttackTower;
    [SerializeField] private Sprite backgroundSupportBuilding;

    [Header("Data Panels")]
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI buildingDescription;
    [SerializeField] private Image buildingImage;
    [SerializeField] private Image buildingShape;
    public GameObject assignedBuilding { get; set; }

    private void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    public void InitializeButtonData(ButtonData data)
    {
        if (assignedBuilding != null)
        {
            if (data.isAttackTower) background.sprite = backgroundAttackTower;
            if (!data.isAttackTower) background.sprite = backgroundSupportBuilding;

            buildingName.text = data.buildingName;
            buildingDescription.text = data.buildingDescription;
            buildingImage.sprite = data.buildingImage;
            buildingShape.sprite = data.buildingShape;
        }
    }

    private void OnButtonClick()
    {
        BuildingManager.Instance.SetActiveBuilding(BuildingPool.Instance.GetBuilding(assignedBuilding.GetComponent<Building>().identifier));

        assignedBuilding = null;
        
        UIManager.Instance.ReturnBuildingsToPool();
    }

    public void HideButton()
    {
        GetComponent<Button>().enabled = false;

        if (background != null) background.enabled = false;
        if (buildingName != null) buildingName.enabled = false;
        if (buildingDescription != null) buildingDescription.enabled = false;
        if (buildingImage != null) buildingImage.enabled = false;
        if (buildingShape != null) buildingShape.enabled = false;
    }
    public void ShowButton()
    {
        GetComponent<Button>().enabled = true;

        if (background != null) background.enabled = true;
        if (buildingName != null) buildingName.enabled = true;
        if (buildingDescription != null) buildingDescription.enabled = true;
        if (buildingImage != null) buildingImage.enabled = true;
        if (buildingShape != null) buildingShape.enabled = true;
    }
}