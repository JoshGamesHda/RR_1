using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoBoxButton : MonoBehaviour
{
    private Button button;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject infoBox;

    void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleOnOff);

        infoBox.SetActive(false);
    }
    private void ToggleOnOff()
    {
        infoBox.SetActive(!infoBox.activeInHierarchy);

        if (infoBox.activeInHierarchy)
            text.color = Color.black;
        else 
            text.color = Color.white;
    }
}
