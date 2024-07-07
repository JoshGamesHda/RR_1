using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Cluster
{
    public List<string> enemies;
    public int unlockAtWave;
    public int lockAtWave;
}

[CreateAssetMenu(fileName = "New ClusterCollection", menuName = "ClusterCollection")]
public class ClusterCollection : ScriptableObject
{
    public List<Cluster> clusters;
}