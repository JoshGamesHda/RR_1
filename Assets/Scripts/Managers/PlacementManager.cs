using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    #region Singleton
    private static PlacementManager instance;
    private PlacementManager() { }
    public static PlacementManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlacementManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("CameraManager");
                    instance = obj.AddComponent<PlacementManager>();
                }
            }
            return instance;
        }
    }
    #endregion
    public bool inactive { get; set; }

    private Building activeBuilding;

    private void OnEnable()
    {
        inactive = true;
    }
    private void Update()
    {
        if (!inactive)
        {
            if (!BuildingManager.Instance.activeBuilding.GetComponent<Building>().isSupport)
            {
                AttackTower tower = (AttackTower) BuildingManager.Instance.activeBuilding.GetComponent<Building>();
                tower.showRangeIndication = true;
            }
            if (InputManager.Instance.hoverCell != null)
            {
                Building curBuilding = BuildingManager.Instance.activeBuilding.GetComponent<Building>();
                Cell hoverCell = InputManager.Instance.hoverCell.GetComponent<Cell>();
                Vector2Int cellPos = InputManager.Instance.hoverCell.GetComponent<Cell>().posInGrid;

                if (curBuilding.GetEffect() == null)
                {
                    AttackTower tower = (AttackTower) curBuilding;
                    tower.PeekEffects(hoverCell);
                    tower.ApplySupportEffects();
                    UIManager.Instance.ShowStatDisplay(tower);
                    tower.UpdateRangeIndication();
                }

                curBuilding.transform.position = Utility.Vec2IntToVec3(cellPos);

                // Actually place the building
                if (Input.GetKeyDown(Constants.KEY_PLACEMENT))
                {
                    if (BuildingManager.Instance.BuildingFits(curBuilding, hoverCell))
                    {
                        UIManager.Instance.HideStatDisplay();
                        InputManager.Instance.ignoreNextSelect = true;

                        SoundManager.Instance.PlayPlaceBuilding();

                        BuildingManager.Instance.PlaceAt(curBuilding, hoverCell);

                        if (!BuildingManager.Instance.activeBuilding.GetComponent<Building>().isSupport)
                        {
                            AttackTower tower = (AttackTower)BuildingManager.Instance.activeBuilding.GetComponent<Building>();
                            tower.HideRangeIndication();
                        }

                        inactive = true;
                    }
                    else Debug.Log("Building don fit here");
                }
            }
            // Find intersection point with XZ
            else
            {
                Vector3 intersectionPoint = Utility.IntersectionPointRayWithXZPlane(CameraManager.Instance.GetCam().ScreenPointToRay(Input.mousePosition));

                BuildingManager.Instance.activeBuilding.GetComponent<Building>().transform.position = new Vector3(intersectionPoint.x, 0, intersectionPoint.z);
            }

            // Rotate Building
            if(Input.GetKeyDown(Constants.KEY_ROTATE_BUILDING))
            {
                SoundManager.Instance.PlayRotateBuilding();
                BuildingManager.Instance.Rotate(BuildingManager.Instance.activeBuilding);
            }
        }
    }
}
