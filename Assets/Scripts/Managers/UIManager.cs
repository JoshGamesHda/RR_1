using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager instance;
    private UIManager() { }
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("CameraManager");
                    instance = obj.AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    // Just leave this public, for whatever reason adding {get; set; } messes up the SerializeField
    public List<GameObject> buttons;

    [Header("General UI")]
    [SerializeField] private GameObject deleteBuilding;
    [SerializeField] private GameObject mountainHealth;
    [SerializeField] private GameObject waveNum;

    #region TowerData
    [Header("Tower Data")]
    [Header("Attack Tower")]
    [SerializeField] private Sprite backgroundAttackTower;
    [SerializeField] private Sprite shapeAttackTower;
    [Header("Support Building")]
    [SerializeField] private Sprite backgroundSupportBuilding;
    [Header("SingleDamage Tower")]
    [SerializeField] private string buildingNameSingleDamage;
    [SerializeField] private string descriptionSingleDamage;
    [SerializeField] private Sprite imageSingleDamage;
    [Header("AOE Tower")]
    [SerializeField] private string buildingNameAOE;
    [SerializeField] private string descriptionAOE;
    [SerializeField] private Sprite imageAOE;
    [Header("FireRate Tower")]
    [SerializeField] private string buildingNameFireRate;
    [SerializeField] private string descriptionFireRate;
    [SerializeField] private Sprite imageFireRate;
    [Header("SpeedUp")]
    [SerializeField] private string buildingNameSpeedUp;
    [SerializeField] private string descriptionSpeedUp;
    [SerializeField] private Sprite imageSpeedUp;
    [SerializeField] private Sprite shapeSpeedUp;
    [Header("DamageUp")]
    [SerializeField] private string buildingNameDamageUp;
    [SerializeField] private string descriptionDamageUp;
    [SerializeField] private Sprite imageDamageUp;
    [SerializeField] private Sprite shapeDamageUp;
    [Header("RangeUp")]
    [SerializeField] private string buildingNameRangeUp;
    [SerializeField] private string descriptionRangeUp;
    [SerializeField] private Sprite imageRangeUp;
    [SerializeField] private Sprite shapeRangeUp;
    #endregion

    private GameObject lastBuilding;

    void OnEnable()
    {
        HideButtons();
        HideDeleteBuildingButton();
        lastBuilding = null;
        
    }
    public void ShowButtons()
    {
        foreach(GameObject button in buttons)
        {
            button.GetComponent<Button>().enabled = true;
            button.GetComponent<Image>().enabled = true;
            button.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        }
    }
    public void HideButtons()
    {
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().enabled = false;
            button.GetComponent<Image>().enabled = false;
            button.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }
    }
    public void RandomizeButtons()
    {
        foreach(GameObject buttonObject in buttons)
        {
            lastBuilding = BuildingPool.Instance.GetRandomBuilding();

            lastBuilding.SetActive(false);

            buttonObject.GetComponent<ButtonScript>().assignedBuilding = lastBuilding;
        }
       ShowButtons();
    }
    public void ReturnBuildingsToPool()
    {
        foreach(GameObject buttonObject in buttons)
        {
            ButtonScript button = buttonObject.GetComponent<ButtonScript>();
            if (button.assignedBuilding == null) continue;

            BuildingPool.Instance.ReturnBuilding(button.assignedBuilding);
        }
    }

    public void ShowDeleteBuildingButton()
    {
        deleteBuilding.SetActive(true);
    }
    public void OnDeleteBuilding()
    {
        BuildingManager.Instance.TrashActiveBuilding();
    }
    public void HideDeleteBuildingButton()
    {
        deleteBuilding.SetActive(false);
    }

    public void UpdateMountainHealthDisplay(int health)
    {
        mountainHealth.GetComponent<MountainHealthUI>().SetMountainHealth(health);
    }
    public void UpdateWaveNumDisplay(int newWaveNum)
    {
        waveNum.GetComponent<WaveNumUI>().SetWaveNum(newWaveNum);
    }
}