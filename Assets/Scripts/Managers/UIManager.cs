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

    #region Fields
    // Just leave this public, for whatever reason adding {get; set; } messes up the SerializeField
    public List<GameObject> buttons;

    [Header("UI elements")]
    [SerializeField] private GameObject deleteBuilding;
    [SerializeField] private GameObject waveNum;
    [SerializeField] private GameObject statDisplay;

    [Header("Mountain Health")]
    [SerializeField] private TextMeshProUGUI mountainHealth;
    [SerializeField] private ProgressBar healthBar;

    [Header("Defeat Screen")]
    [SerializeField] private GameObject defeatScreen;

    [Header("Menu")]
    [SerializeField] private GameObject menu;

    private GameObject lastBuilding;
    #endregion

    void OnEnable()
    {
        HideButtons();
        HideDeleteBuildingButton();
        HideStatDisplay();
        HideDefeatScreen();

        healthBar.InitializeBar(GameData.Instance.initialMountainHP);
        lastBuilding = null;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleMenu();
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
        SoundManager.Instance.PlayTrashCan();

        BuildingManager.Instance.TrashActiveBuilding();
    }
    public void HideDeleteBuildingButton()
    {
        deleteBuilding.SetActive(false);
    }

    public void UpdateMountainHealthDisplay(int health)
    {
        healthBar.UpdateBar(health);
        mountainHealth.text = health.ToString();
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

    public void ShowDefeatScreen()
    {
        defeatScreen.SetActive(true);
    }
    public void HideDefeatScreen()
    {
        defeatScreen.SetActive(false);
    }

    private void ToggleMenu()
    {
        menu.SetActive(!menu.activeInHierarchy);
    }
}