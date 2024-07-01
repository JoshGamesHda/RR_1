using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    #region Singleton
    private static GameData instance;
    private GameData() { }
    public static GameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameData>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("GameData");
                    instance = obj.AddComponent<GameData>();
                }
            }
            return instance;
        }
    }
    #endregion

    #region TowerChoices
    [Header("Available Towers")]
    public bool SingleDamageTower;
    public bool AoeTower;
    public bool FireRateTower;
    public bool SpeedUpBuilding;
    public bool DamageUpBuilding;
    public bool RangeUpBuilding;
    #endregion

    #region Mountain-/ WaveData

    [Header("General Stats")]
    // Distance from Wave Centers to Mountain
    public float waveDistanceToPlateau;

    // How many red spheres are generated each wave
    public int areasPerWave;
    public float areaRadius;

    // How many clusters are distributed over the waves
    public int clustersPerWave;

    // The Duration until all enemies of one wave are sent
    public float waveDuration;


    public float initialMountainHP;
    #endregion

    #region Support Stats
    [Header("Support Stats")]
    public float SpeedUp_Multiplier;
    public float DamageUp_Multiplier;
    public float RangeUp_Multiplier;
    #endregion

    #region Raw Tower Stats
    [Header("Raw Tower Stats")]
    [Header("All Projectiles")]
    public float projectileLifetime;

    [Header("High SingleDamage Ballista")]
    public float rawDamageBallista;
    public float rawFireRateBallista;
    public float rawRangeBallista;
    public float CandySpikeProjSpeed;

    [Header("AOE Tower")]
    public float rawDamageAOE;
    public float rawFireRateAOE;
    public float rawRangeAOE;
    public float AoeProjSpeed;
    public float AoeRadius;

    [Header("High FireRate Tower")]
    public float rawDamageFireRate;
    public float rawFireRateFireRate;
    public float rawRangeFireRate;
    public float CandyCornProjSpeed;
    #endregion

    #region Enemy Stats
    [Header("Enemy Stats")]
    [Header("Strawberry")]
    public float HPStrawberry;
    public float AttackDamageStrawberry;
    public float AttackRateStrawberry;
    public float AttackRangeStrawberry;
    public float MoveSpeedStrawberry;

    [Header("Lemon")]
    public float HPLemon;
    public float AttackDamageLemon;
    public float AttackRateLemon;
    public float AttackRangeLemon;
    public float MoveSpeedLemon;
    public float ProjSpeedLemon;
    #endregion

    public void UpdateOnceAfterEachWave()
    {
        clustersPerWave = 1 + GameManager.Instance.waveNum * 2;
        if (GameManager.Instance.waveNum == 1)
            areasPerWave = 2;
        if(GameManager.Instance.waveNum == 5)
            areasPerWave = 3;
    }
}
