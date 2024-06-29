using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct Prefab
{
    public string identifier;
    public GameObject prefab;
    public int amountToPool;
}

public class ProjectilePool : MonoBehaviour
{
    #region Singleton
    public static ProjectilePool Instance;
    private ProjectilePool() { }
    #endregion

    #region Fields
    [SerializeField] private List<Prefab> projectilePrefabs;

    private List<(string identifier, Queue<GameObject> projectileObjects)> projectilePool;

    private int projectileTypeCount;
    #endregion

    private void Awake()
    {
        Instance = this;
        projectilePool = new();

        projectileTypeCount = projectilePrefabs.Count;
    }
    void OnEnable()
    {
        for (int i = 0; i < projectileTypeCount; i++)
        {
            projectilePool.Add((projectilePrefabs[i].identifier, new()));

            for (int p = 0; p < projectilePrefabs[i].amountToPool; p++)
            {
                GameObject projectile = Instantiate(projectilePrefabs[i].prefab);
                projectile.SetActive(false);
                projectile.transform.SetParent(transform);

                projectilePool[i].projectileObjects.Enqueue(projectile);
            }
        }
    }

    public GameObject GetProjectile(string identifier)
    {
        
        for (int i = 0; i < projectileTypeCount; i++)
        {
            if (projectilePool[i].identifier == identifier)
            {
                if (projectilePool[i].projectileObjects.Count != 0)
                {
                    GameObject projectile = projectilePool[i].projectileObjects.Dequeue();
                    projectile.SetActive(true);

                    return projectile;
                }
                else
                {
                    Debug.Log("ProjectilePool empty");
                    GameObject extraProjectile = Instantiate(projectilePrefabs[i].prefab);
                    extraProjectile.transform.SetParent(transform);

                    return extraProjectile;
                }
            }
        }
        Debug.Log("Didn't find projectile");
        return null;
    }
    public void ReturnProjectile(GameObject projectile)
    {
        string identifier = projectile.GetComponent<Projectile>().identifier;
        for (int i = 0; i < projectileTypeCount; i++)
        {
            if (projectilePool[i].identifier == identifier)
            {
                projectile.SetActive(false);
                projectilePool[i].projectileObjects.Enqueue(projectile);

                return;
            }
        }
        Debug.Log("Could not return projectile to pool");
    }
}
