using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour, IHoverable
{
    public Vector2Int posInGrid { get; set; }
    public Building buildingOnCell { get; set; }

    private void Update()
    {
        if (buildingOnCell != null) GetComponent<Renderer>().material.color = Color.red;
        if (buildingOnCell == null) GetComponent<Renderer>().material.color = Color.white;
    }

    public void OnHover()
    {

    }
}
