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

    private int? lastIndex;
    public GameObject GetRandomBuilding()
    {
        int towerAmount = buildingTypeCount;

        List<string> availableBuildings = new();

        #region the ifs
        if (GameData.Instance.SingleDamageTower) availableBuildings.Add("SingleDamage");
        else towerAmount--;

        if (GameData.Instance.AoeTower) availableBuildings.Add("AOE");
        else towerAmount--;

        if (GameData.Instance.FireRateTower) availableBuildings.Add("FireRate");
        else towerAmount--;

        if (GameData.Instance.SpeedUpBuilding) availableBuildings.Add("SpeedUp");
        else towerAmount--;

        if (GameData.Instance.DamageUpBuilding) availableBuildings.Add("DamageUp");
        else towerAmount--;

        if (GameData.Instance.RangeUpBuilding) availableBuildings.Add("RangeUp");
        else towerAmount--;
        #endregion

        int rand;
        if (lastIndex != null)
        {
            rand = UnityEngine.Random.Range(0, availableBuildings.Count - 1);
            if (rand > lastIndex) rand += 1;
        }
        else rand = UnityEngine.Random.Range(0, availableBuildings.Count);
        lastIndex = rand;

        if (GameManager.Instance.waveNum == 0 && buildingTypeCount == towerAmount) rand = rand % 3;
        
        return GetBuilding(availableBuildings[rand]);
    }
}