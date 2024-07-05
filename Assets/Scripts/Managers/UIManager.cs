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

    [Header("UI elements")]
    [SerializeField] private GameObject deleteBuilding;
    [SerializeField] private GameObject mountainHealth;
    [SerializeField] private GameObject waveNum;
    [SerializeField] private GameObject statDisplay;

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
            button.GetComponent<ButtonScript>().ShowButton();
        }
    }
    public void HideButtons()
    {
        foreach (GameObject button in buttons)
        {
            button.GetComponent<ButtonScript>().HideButton();
        }
    }
    public void RandomizeButtons()
    {
        foreach(GameObject buttonObject in buttons)
        {
            lastBuilding = BuildingPool.Instance.GetRandomBuilding();

            lastBuilding.SetActive(false);

            buttonObject.GetComponent<ButtonScript>().assignedBuilding = lastBuilding;
            buttonObject.GetComponent<ButtonScript>().InitializeButtonData(lastBuilding.GetComponent<Building>().buttonData);
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

    public void ShowStatDisplay(Building building)
    {
        statDisplay.GetComponent<StatDisplay>().UpdateStatDisplay(building);
        statDisplay.GetComponent<StatDisplay>().ShowStatDisplay();
    }
    public void HideStatDisplay()
    {
        statDisplay.GetComponent<StatDisplay>().HideStatDisplay();
    }
}