using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    #region Singleton
    private static BuildingManager instance;
    private BuildingManager() { }
    public static BuildingManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BuildingManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("CameraManager");
                    instance = obj.AddComponent<BuildingManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    #region Fields
    [SerializeField] private GameObject buildingPool;
    private List<GameObject> placedBuildings;
    // The click of one of the buttons should set activeBuilding to a Building
    public GameObject activeBuilding { get; set; }

    // When clicking on a building in Preparation Phase
    private GameObject selectedBuilding;
    #endregion

    private void OnEnable()
    {
        placedBuildings = new();
    }

    public void PlaceAt(Building building, Cell cell)
    {
        activeBuilding.transform.SetParent(cell.transform, true);
        activeBuilding.GetComponent<Building>().placed = true;

        Building b = activeBuilding.GetComponent<Building>();

        // Give Cells to Blocks and Blocks to Cells
        for (int i = 0; i < b.blocks.Count; i++)
        {
            Cell cellBelowBlock = PlateauManager.Instance.GetCell(cell.posInGrid.x + b.blocks[i].pos.x, cell.posInGrid.y + b.blocks[i].pos.y).GetComponent<Cell>();
            cellBelowBlock.buildingOnCell = building;

            Block newBlock = b.blocks[i];
            newBlock.cell = cellBelowBlock;
            b.blocks[i] = newBlock;
        }

        placedBuildings.Add(activeBuilding);
        ApplyEffects();
    }
    public void PickUpFrom(Cell cell)
    {
        int buildingCount = placedBuildings.Count;
        // Cycle through all buildings currently on the grid
        for (int i = buildingCount - 1; i >= 0; i--)
        {
            bool targetBuilding = false;

            // Check if one Block of the current building sits on the given Cell
            foreach (Block block in placedBuildings[i].GetComponent<Building>().blocks)
            {
                if (block.cell == cell)
                { 
                    targetBuilding = true;
                    break;
                }
            }

            // if we found the Block, we must be at the right index in the big foreach loop, we wanna pick the current index up
            if (targetBuilding)
            {
                Building chosenBuilding = placedBuildings[i].GetComponent<Building>();

                SetActiveBuilding(placedBuildings[i]);

                foreach (Block block in chosenBuilding.blocks)
                {
                    block.cell.buildingOnCell = null;
                }

                placedBuildings.RemoveAt(i);

                PlacementManager.Instance.inactive = false;
                return;
            }
            else Debug.Log("Building not found");
        }
    }
    public void Rotate(GameObject building)
    {
        building.GetComponent<Building>().Rotate();
    }
    public bool BuildingFits(Building building, Cell cell)
    {
        foreach(Block block in building.blocks)
        {
            GameObject curCell = PlateauManager.Instance.GetCell(cell.posInGrid.x + block.pos.x, cell.posInGrid.y + block.pos.y);
            if (curCell == null) return false;
            if (curCell.GetComponent<Cell>().buildingOnCell != null) return false;
        }
        return true;
    }

    public void SetActiveBuilding(GameObject building)
    {
        activeBuilding = building;
    }
    public void TrashActiveBuilding()
    {
        BuildingPool.Instance.ReturnBuilding(activeBuilding);
        PlacementManager.Instance.inactive = true;
    }

    public void ApplyEffects()
    {
        foreach (GameObject building in placedBuildings)
        {
            AttackTower tower = building.GetComponent<AttackTower>();
            if (tower != null)
            {
                tower.UpdateSupportEffects();
                tower.ApplySupportEffects();
            }
        }
    }

    public void SelectBuilding(GameObject building)
    {
        UIManager.Instance.ShowStatDisplay(building.GetComponent<Building>());
    }
    public void UnselectBuilding()
    {
        UIManager.Instance.HideStatDisplay();
    }
}
