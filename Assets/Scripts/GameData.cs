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
    public float clustersPerWave;

    // The Duration until all enemies of one wave are sent
    public float waveDuration;


    public float initialMountainHP;
    #endregion

    #region Support Stats
    [Header("Support Stats")]
    public float speedUp_Multiplier;
    public float damageUp_Multiplier;
    public float rangeUp_Multiplier;
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

    [Header("Blueberry")]
    public float HPBlueberry;
    public float AttackDamageBlueberry;
    public float AttackRateBlueberry;
    public float AttackRangeBlueberry;
    public float MoveSpeedBlueberry;

    [Header("KingDurian")]
    public float HPDurian;
    public float AttackDamageDurian;
    public float AttackRateDurian;
    public float AttackRangeDurian;
    public float MoveSpeedDurian;
    #endregion

    public void InitializeData()
    {
        clustersPerWave = 1;
    }

    public void UpdateOnceAfterEachWave()
    {
        if (GameManager.Instance.waveNum < 16)
            clustersPerWave = 1 + GameManager.Instance.waveNum * 2;

        if (GameManager.Instance.waveNum < 12)
            clustersPerWave = 1 + (GameManager.Instance.waveNum - 2) * 2;

        if (GameManager.Instance.waveNum < 8)
            clustersPerWave = 1 + GameManager.Instance.waveNum;


        if (GameManager.Instance.waveNum < 4)
            clustersPerWave = 3;
        if (GameManager.Instance.waveNum < 2)
            clustersPerWave = 1;


        if (GameManager.Instance.waveNum == 2)
            areasPerWave = 2;
        if (GameManager.Instance.waveNum == 7)
            areasPerWave = 3;
        if (GameManager.Instance.waveNum == 18)
            areasPerWave = 4;
        if (GameManager.Instance.waveNum == 23)
            areasPerWave = 5;

        //if (GameManager.Instance.waveNum == 12)
        //    HPStrawberry += 2;
        //if (GameManager.Instance.waveNum == 12)S
        //    HPLemon += 2;

        //Rubberbanding:
        //if player hp < 100 -HPLemon gegner

        if (GameManager.Instance.waveNum == 3)
            waveDuration = 15;

        //GameManager.Instance.mountain.GetComponent<Mountain>().ResetHP();
    }
}
