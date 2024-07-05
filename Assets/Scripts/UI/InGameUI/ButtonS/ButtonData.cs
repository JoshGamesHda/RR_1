using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonData : ScriptableObject
{
    public bool isAttackTower { get; set; }
    public string identifier { get; set; }
    public string buildingName { get; set; }
    public string buildingDescription { get; set;}

    public Sprite buildingImage { get; set;}
    public Sprite buildingShape { get; set;}
}
