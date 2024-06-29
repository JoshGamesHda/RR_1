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
    [SerializeField]
    public List<GameObject> buttons;

    [SerializeField] private 
        GameObject deleteBuilding;

    private GameObject lastBuilding;
    public bool buttonsShown { get; private set; }

    void OnEnable()
    {
        HideButtons();
        HideDeleteBuildingButton();
        buttonsShown = false;
        lastBuilding = null;
        
    }
    public void ShowButtons()
    {
        buttonsShown = true;
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().enabled = true;
            button.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            button.GetComponent<Image>().enabled = true;
        }
    }
    public void HideButtons()
    {
        buttonsShown = false;
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().enabled = false;
            button.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            button.GetComponent<Image>().enabled = false;
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
}