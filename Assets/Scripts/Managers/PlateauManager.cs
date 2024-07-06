using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateauManager : MonoBehaviour
{
    #region Singleton
    private static PlateauManager instance;
    private PlateauManager() { }
    public static PlateauManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlateauManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("CameraManager");
                    instance = obj.AddComponent<PlateauManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private PlateauStructure structure;
    [SerializeField]
    private GameObject cellPrefab;

    private List<GameObject> cells;
    public List<GameObject> Cells => cells;

    void OnEnable()
    {
        cells = new();

        List<GridPos> posInStructure = structure.grid;

        for (int i = 0; i < posInStructure.Count; i++)
        {
            // Instantiates a Cell Prefab at the position made up of the Plateau GameObject's position with the local grid offset of the current Cell from the GridStructure list
            // Simultaneously, we get the Cell script component of that freshly instantiated prefab and store it in the variable curCell
            Cell curCell = Instantiate(cellPrefab, new Vector3(posInStructure[i].x, 0, posInStructure[i].y), Quaternion.identity).GetComponent<Cell>();

            // We call the self-made constructor of the Cell script and tell it its own position in the grid
            curCell.posInGrid = new Vector2Int(posInStructure[i].x, posInStructure[i].y);

            // Setting the transform of each cell to the parent for more clarity in the hierarchy
            curCell.transform.SetParent(transform);

            // Finally, we add the Cell GameObject to ousr cells list, so the PlateauScript always has access to all cells that are parented to it
            cells.Add(curCell.gameObject);
        }
    }

    public GameObject GetCell(int x, int y)
    {
        foreach(GameObject cell in cells)
        {
            if (cell.GetComponent<Cell>().posInGrid == new Vector2(x, y)) return cell;
        }
        return null;
    }
}
