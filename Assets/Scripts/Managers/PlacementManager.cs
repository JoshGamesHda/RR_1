using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
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

    public bool inactive { get; set; }

    int rangeIndicatorsAmount = 12;
    List<GameObject> rangeIndicators;
    private void OnEnable()
    {
        inactive = true;

        InitializeRangeIndicators();
    }
    private void Update()
    {
        if (!inactive)
        {
            ActivateRangeIndicators();
            UpdateRangeIndicators();
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
                    UpdateRangeIndicators();
                }

                curBuilding.transform.position = Utility.Vec2IntToVec3(cellPos);

                if (Input.GetMouseButtonDown(0))
                {
                    if (BuildingManager.Instance.BuildingFits(curBuilding, hoverCell))
                    {
                        BuildingManager.Instance.PlaceAt(curBuilding, hoverCell);

                        DeactivateRangeIndicators();

                        inactive = true;
                    }
                    else Debug.Log("Building don fit here");
                }
            }
            else
            {
                Vector3 intersectionPoint = Utility.IntersectionPointRayWithXZPlane(CameraManager.Instance.GetCam().ScreenPointToRay(Input.mousePosition));

                BuildingManager.Instance.activeBuilding.GetComponent<Building>().transform.position = new Vector3(intersectionPoint.x, 0, intersectionPoint.z);
            }
            if(Input.GetKeyDown(Constants.KEY_ROTATE_BUILDING))
            {
                BuildingManager.Instance.Rotate(BuildingManager.Instance.activeBuilding);
            }
        }
    }

    private void InitializeRangeIndicators()
    {
        rangeIndicators = new();

        for (int i = 0; i < rangeIndicatorsAmount; i++)
        { 
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            cube.GetComponent<Renderer>().material.color = Color.blue;

            cube.transform.SetParent(transform, false);

            cube.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

            cube.SetActive(false);

            rangeIndicators.Add(cube);
        }
    }

    private void UpdateRangeIndicators()
    {
        if (BuildingManager.Instance.activeBuilding.HasComponent<AttackTower>())
        {
            float increment = (2 * Mathf.PI) / rangeIndicatorsAmount;

            float range = BuildingManager.Instance.activeBuilding.GetComponent<AttackTower>().range;

            for (int i = 0; i < rangeIndicatorsAmount; i++)
            {
                rangeIndicators[i].transform.localPosition = new Vector3(Mathf.Sin(increment * i), 0, Mathf.Cos(increment * i)) * range;
            }
        }
    }

    private void ActivateRangeIndicators()
    {
        if (!rangeIndicators[0].activeSelf)
        {
            foreach (GameObject r in rangeIndicators)
            {
                r.transform.SetParent(BuildingManager.Instance.activeBuilding.transform, false);
                r.SetActive(true);
            }
        }
    }
    private void DeactivateRangeIndicators()
    {
        if (rangeIndicators[0].activeSelf)
        {
            foreach (GameObject r in rangeIndicators)
            {
                r.SetActive(false);
            }
        }
    }
}
