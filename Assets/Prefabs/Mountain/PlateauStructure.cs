using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GridPos
{
    public int x, y;
}

[CreateAssetMenu(fileName = "New PlateauStructure", menuName = "Mountain/PlateauStructure")]
public class PlateauStructure : ScriptableObject
{
    // Do not {get; set; } this, it will destroy your plateau
    public List<GridPos> grid;
}
