using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct BuildingPrefab
{
    public string identifier;
    public GameObject prefab;
}
public class BuildingPool : MonoBehaviour
{
    #region Singleton
    public static BuildingPool Instance;
    private BuildingPool() { }
    #endregion

    #region Fields
    [SerializeField] private List<BuildingPrefab> buildingPrefabs;

    private List<(string identifier, Queue<GameObject> buildingObjects)> buildingPool;
    [SerializeField] private int buildingsPerPool;

    private int buildingTypeCount;
    #endregion

    private void Awake()
    {
        Instance = this;
        buildingPool = new();

        buildingTypeCount = buildingPrefabs.Count;
    }
    void OnEnable()
    {
        for (int i = 0; i < buildingTypeCount; i++)
        {
            buildingPool.Add((buildingPrefabs[i].identifier, new()));

            for (int p = 0; p < buildingsPerPool; p++)
            {
                GameObject building = Instantiate(buildingPrefabs[i].prefab);
                building.SetActive(false);
                building.transform.SetParent(transform);

                buildingPool[i].buildingObjects.Enqueue(building);
            }
        }
    }

    public GameObject GetBuilding(string identifier)
    {
        for (int i = 0; i < buildingTypeCount; i++)
        {
            if (buildingPool[i].identifier == identifier)
            {
                if (buildingPool[i].buildingObjects.Count != 0)
                {
                    GameObject enemy = buildingPool[i].buildingObjects.Dequeue();
                    enemy.SetActive(true);

                    return enemy;
                }
                else
                {
                    GameObject extraBuilding = Instantiate(buildingPrefabs[i].prefab);
                    extraBuilding.transform.SetParent(transform);

                    return extraBuilding; 
                }
            }
        }
        Debug.Log("Didn't find building");
        return null;
    }
    public void ReturnBuilding(GameObject building)
    {
        string identifier = building.GetComponent<Building>().identifier;
        for (int i = 0; i < buildingTypeCount; i++)
        {
            if (buildingPool[i].identifier == identifier)
            {
                building.SetActive(false);
                buildingPool[i].buildingObjects.Enqueue(building);
                return;
            }
        }
        Debug.Log("Could not return building to pool");
    }

    public string lastPickedBuilding { private get; set; }
    private Queue<string> lastBuildings = new();
    public GameObject GetRandomBuilding()
    {
        int towerAmount = buildingTypeCount;

        int attackTowers = 0;

        List<string> availableBuildings = new();

        #region the ifs
        if (GameData.Instance.SingleDamageTower && lastPickedBuilding != Constants.ID_SINGLE_DAMAGE && !lastBuildings.Contains(Constants.ID_SINGLE_DAMAGE))
        {
            availableBuildings.Add(Constants.ID_SINGLE_DAMAGE);
            attackTowers++;
        }
        else towerAmount--;

        if (GameData.Instance.AoeTower && lastPickedBuilding != Constants.ID_AOE && !lastBuildings.Contains(Constants.ID_AOE))
        {
            availableBuildings.Add(Constants.ID_AOE);
            attackTowers++;
        }
        else towerAmount--;

        if (GameData.Instance.FireRateTower && lastPickedBuilding != Constants.ID_FIRERATE && !lastBuildings.Contains(Constants.ID_FIRERATE))
        {
            attackTowers++;
            availableBuildings.Add(Constants.ID_FIRERATE);
        }
        else towerAmount--;

        if (GameData.Instance.SpeedUpBuilding && lastPickedBuilding != Constants.ID_SPEEDUP && !lastBuildings.Contains(Constants.ID_SPEEDUP))
        {
            availableBuildings.Add(Constants.ID_SPEEDUP);
        }

        else towerAmount--;

        if (GameData.Instance.DamageUpBuilding && lastPickedBuilding != Constants.ID_DAMAGEUP && !lastBuildings.Contains(Constants.ID_DAMAGEUP))
        {
            availableBuildings.Add(Constants.ID_DAMAGEUP);
        }
        else towerAmount--;

        if (GameData.Instance.RangeUpBuilding && lastPickedBuilding != Constants.ID_RANGEUP && !lastBuildings.Contains(Constants.ID_RANGEUP))
        {
                availableBuildings.Add(Constants.ID_RANGEUP);
        }
        else towerAmount--;
        #endregion

        int rand;
        if (GameManager.Instance.waveNum == 0)
        {
            rand = UnityEngine.Random.Range(0, availableBuildings.Count);
            rand %= attackTowers;
            return GetBuilding(availableBuildings[rand]);
        }

        rand = UnityEngine.Random.Range(0, availableBuildings.Count);

        if(lastBuildings.Count < 3) lastBuildings.Enqueue(availableBuildings[rand]);
        else
        {
            lastBuildings.Dequeue();
            lastBuildings.Enqueue(availableBuildings[rand]);
        }
        return GetBuilding(availableBuildings[rand]);
    }
}