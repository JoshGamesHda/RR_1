using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region Singleton
    private static WaveManager instance;
    private WaveManager() { }
    public static WaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WaveManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("WaveManager");
                    instance = obj.AddComponent<WaveManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    [SerializeField] private float waveDurationInSec;

    private Queue<SpawningArea> areaCollection;
    private List<(Queue<string> enemyTypes, Queue<Vector2> pos, float frequency, float lastSpawnTime)> enemiesToSpawn;

    private float waveStart;

    private List<GameObject> enemies;
    private bool allEnemiesSent;

    private bool waveSurvived;
    public bool waveActive { get; private set; }

    private void OnEnable()
    {
        enemiesToSpawn = new();
        areaCollection = new();
        enemies = new();

        waveActive = false;
    }

    private void Update()
    {
        if (waveActive)
        {
            if (Input.GetKeyDown(KeyCode.P)) KillAllEnemies();
            // each cluster sends in their enemies seperate from each other
            int clusterCount = enemiesToSpawn.Count;
            int clustersSent = 0;

            for (int i = 0; i < clusterCount; i++)
            {
                if (enemiesToSpawn[i].enemyTypes.Count == 0)
                { 
                    clustersSent++;
                    continue;
                }
                if (Time.time >= enemiesToSpawn[i].lastSpawnTime + enemiesToSpawn[i].frequency)
                {
                    GameObject enemy = EnemyPool.Instance.GetEnemy(enemiesToSpawn[i].enemyTypes.Dequeue());
                    Vector2 pos = enemiesToSpawn[i].pos.Dequeue();
                    enemy.transform.position = new Vector3(pos.x, 0, pos.y);
                    enemies.Add(enemy);

                    // Update the last spawn time for this cluster
                    enemiesToSpawn[i] = (enemiesToSpawn[i].enemyTypes, enemiesToSpawn[i].pos, enemiesToSpawn[i].frequency, Time.time);
                }
            }
            if (clustersSent == clusterCount) allEnemiesSent = true;

            RemoveInactiveEnemies();

            // If theres no more enemies coming and all are removed from the list, the wave is over
            if (allEnemiesSent && enemies.Count == 0)
            {
                allEnemiesSent = false;

                waveSurvived = true;
                waveActive = false;
            }
            if (!GameManager.Instance.mountain.GetComponent<Mountain>().IsAlive())
            {
                waveSurvived = false;
                waveActive = false; 
            }
        }
    }

    public void StartWave()
    {
        InitializeWaveData();
        waveStart = Time.time;
        waveActive = true;
    }

    public void QueueArea(SpawningArea area)
    {
        areaCollection.Enqueue(area);
    }

    public bool GetWaveSurvived()
    {
        return waveSurvived;
    }
    public void KillAllEnemies()
    {
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(1000000000000000);
        }
    }
    private void InitializeWaveData()
    {
        enemiesToSpawn.Clear();

        int enemyAmountThisCluster;

        while (areaCollection.Count > 0)
        {
            SpawningArea curArea = areaCollection.Dequeue();
            int clusterCount = curArea.clusters.Count;
            for (int i = 0; i < clusterCount; i++)
            {
                enemyAmountThisCluster = curArea.clusters[i].enemies.Count;

                Queue<string> enemyTypes = new();
                Queue<Vector2> pos = new();
                float frequency = waveDurationInSec / enemyAmountThisCluster;
                float lastSpawnTime = waveStart;

                for (int i2 = 0; i2 < enemyAmountThisCluster; i2++)
                {
                    enemyTypes.Enqueue(curArea.clusters[i].enemies[i2]);
                    pos.Enqueue(Utility.RandPosOnCircle(Utility.Vec3ToVec2(curArea.pos), GameData.areaRadius));
                }

                enemiesToSpawn.Add((enemyTypes, pos, frequency, lastSpawnTime));
            }
        }
    }

    private void RemoveInactiveEnemies()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].activeInHierarchy)
            { 
                enemies.RemoveAt(i);
                i--;
            }
        }
    }
}
