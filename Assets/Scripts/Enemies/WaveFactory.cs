using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


public class WaveFactory : MonoBehaviour
{
    [SerializeField] ClusterCollection clusterCollection;

    public Wave CreateWave(int waveNum)
    {
        List<float> rotations = new();
        for (int i = 0; i < GameData.Instance.areasPerWave; i++)
            rotations.Add(Random.Range(0f, 2 * Mathf.PI));

        return Wave.Create(rotations, waveNum, clusterCollection);
    }
}

public struct SpawningArea
{
    private float rotationFromPlateau;
    public Vector3 pos { get; private set; }
    public List<Cluster> clusters { get; set; }

    private SpawningArea(float rotatFromPlateau)
    {
        rotationFromPlateau = rotatFromPlateau;
        pos = new Vector3(Mathf.Sin(rotationFromPlateau) * GameData.Instance.waveDistanceToPlateau, 0, Mathf.Cos(rotationFromPlateau) * GameData.Instance.waveDistanceToPlateau);
        clusters = new();
    }

    public static SpawningArea CreateArea(float rotatFromPlateau)
    {
        SpawningArea me = new SpawningArea(rotatFromPlateau);

        return me;
    }

    public void QueueSelf()
    {
        WaveManager.Instance.QueueArea(this);
    }
}

public class Wave
{
    private ClusterCollection clusterCollection;

    public List<SpawningArea> areas { get; private set; }



    public static Wave Create(List<float> rotations, int waveNum, ClusterCollection clusters)
    {
        Wave newWave = new();
        newWave.clusterCollection = clusters;


        newWave.CreateSpawningAreas(rotations);
        newWave.FillClusters(waveNum);

        return newWave;
    }
    private void CreateSpawningAreas(List<float> rotations)
    {
        List<SpawningArea> list = new();

        for (int i = 0; i < GameData.Instance.areasPerWave; i++)
        {
            list.Add(SpawningArea.CreateArea(rotations[i]));
        }
        areas = list;
    }
    private void FillClusters(int waveNum)
    {
        // Extract a list of valid clusters for the current Wave number
        List<Cluster> possibleClusters = new();
        Cluster bossCluster = new Cluster();
        bool bossClusterFound = false;
        for (int i = 0; i < clusterCollection.clusters.Count; i++)
        {
            if (clusterCollection.clusters[i].unlockAtWave <= waveNum && clusterCollection.clusters[i].lockAtWave > waveNum && !clusterCollection.clusters[i].bossCluster) 
                possibleClusters.Add(clusterCollection.clusters[i]);
            if (clusterCollection.clusters[i].unlockAtWave <= waveNum && clusterCollection.clusters[i].lockAtWave > waveNum && clusterCollection.clusters[i].bossCluster)
            {
                bossCluster = clusterCollection.clusters[i];
                bossClusterFound = true;
            }
        }

        for (int i = 0; i < GameData.Instance.clustersPerWave; i++)
        {
            if (bossClusterFound && i == 0)
            {
                areas[Random.Range(0, areas.Count)].clusters.Add(bossCluster);
                continue;
            }

            if (possibleClusters.Count > 0)
            {
                Cluster cluster = new Cluster();
                // Takes out one random cluster from the cluster list
                cluster = possibleClusters[Random.Range(0, possibleClusters.Count)];

                // Making sure each area gets at least one cluster, then the rest will be randomly distributed over the areas
                if (i < areas.Count) areas[i].clusters.Add(cluster);
                else areas[Random.Range(0, areas.Count)].clusters.Add(cluster);
            }
            else break;
        }
    }
    public void SendWave()
    {
        for(int i = 0; i < areas.Count; i++)
        {
            areas[i].QueueSelf();
        }
        WaveManager.Instance.StartWave();
    }
}
