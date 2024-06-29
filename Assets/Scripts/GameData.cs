using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *  All FireRATE, AttackRATEs, etc. are calculated in their value/second
 * 
 * 
 * 
 * 
 * 
 */



public static class GameData
{
    // Distance from Wave Centers to Mountain
    public static readonly float waveDistanceToPlateau = 40f;

    // How many red spheres are generated each wave
    public static readonly int areasPerWave = 2;
    public static readonly float areaRadius = 5f;

    // How many clusters are distributed over the waves
    public static readonly int clustersPerWave = 3;

    public static float INIT_HP = 1000;

    // 
    //        Support Stats
    //

    public static float SpeedUp_Multiplier = 1.5f;
    public static float DamageUp_Multiplier = 1.5f;
    public static float RangeUp_Multiplier = 1.5f;

    // 
    //        Raw Tower Stats
    //

    // Overall projectile lifetime
    public static float projectileLifetime = 3f;

    // Single Damage Ballista thingy
    public static float rawDamageBallista = 10;
    public static float rawFireRateBallista = 0.25f;
    public static float rawRangeBallista = 25;
    public static float CandySpikeProjSpeed = 0.4f;

    // AOE tower
    public static float rawDamageAOE = 3;
    public static float rawFireRateAOE = 0.5f;
    public static float rawRangeAOE = 20;
    public static float AoeProjSpeed = 0.05f;
    public static float AoeRadius = 7.5f;

    // FireRate thing
    public static float rawDamageFireRate = 2;
    public static float rawFireRateFireRate = 2;
    public static float rawRangeFireRate = 15;
    public static float CandyCornProjSpeed = 0.125f;

    // 
    //        Enemy Stats
    //

    // Strawberry
    public static float HPStrawberry = 10;
    public static float AttackDamageStrawberry = 5;
    public static float AttackRateStrawberry = 2;
    public static float AttackRangeStrawberry = 1;
    public static float MoveSpeedStrawberry = 3;

    // Lemon
    public static float HPLemon = 7.5f;
    public static float AttackDamageLemon = 2;
    public static float AttackRateLemon = 1.5f;
    public static float AttackRangeLemon = 8;
    public static float MoveSpeedLemon = 2;
    public static float ProjSpeedLemon = 0.1f;
}