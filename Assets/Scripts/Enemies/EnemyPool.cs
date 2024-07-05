using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct EnemyPrefab
{
    public string identifier;
    public GameObject prefab;
}

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;
    private EnemyPool() { }

    [SerializeField] private List<EnemyPrefab> enemyPrefabs;

    private List<(string identifier, Queue<GameObject> enemyObjects)> enemyPool;
    [SerializeField] private int enemiesPerPool;

    private int enemyTypeCount;
    private void Awake()
    {
        Instance = this;
        enemyPool = new();

        enemyTypeCount = enemyPrefabs.Count;
    }
    void OnEnable()
    {
        for (int i = 0; i < enemyTypeCount; i++)
        {
            enemyPool.Add((enemyPrefabs[i].identifier, new()));

            for (int p = 0; p < enemiesPerPool; p++)
            {
                GameObject enemy = Instantiate(enemyPrefabs[i].prefab);
                enemy.SetActive(false);
                enemy.transform.SetParent(transform);

                enemyPool[i].enemyObjects.Enqueue(enemy);
            }
        }
    }

    public GameObject GetEnemy(string identifier)
    {
        for(int i = 0; i < enemyTypeCount; i++)
        {
            if (enemyPool[i].identifier == identifier)
            {
                if (enemyPool[i].enemyObjects.Count != 0)
                {
                    GameObject enemy = enemyPool[i].enemyObjects.Dequeue();
                    enemy.SetActive(true);
                    enemy.GetComponent<Enemy>().ResetHealthBar();

                    return enemy;
                }
                else
                {
                    GameObject extraEnemy = Instantiate(enemyPrefabs[i].prefab);
                    extraEnemy.transform.SetParent(transform);

                    return extraEnemy;
                }
            }
        }
        Debug.Log("Didn't find enemy");
        return null;
    }
    public void ReturnEnemy(string identifier, GameObject enemy)
    {
        if (enemy.HasComponent<Enemy>())
        {
            for (int i = 0; i < enemyTypeCount; i++)
            {
                if (enemyPool[i].identifier == identifier)
                {
                    enemy.SetActive(false);
                    enemyPool[i].enemyObjects.Enqueue(enemy);
                    return;
                }
            }
            Debug.Log("Could not return enemy to pool");
        }
        Debug.Log("This aint no enemy");
    }
}