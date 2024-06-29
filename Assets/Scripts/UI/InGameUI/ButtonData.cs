using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonData : ScriptableObject
{
    public Building buildingType { get; set; }

    public string identifier { get; set; }
    public string buildingName { get; set; }
    public string buildingDescription { get; set;}

    public Image towerImage { get; set;}
    public Image towerShape { get; set;}
}
