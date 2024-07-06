using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRate : AttackTower
{
    protected override void OnEnable()
    {
        base.OnEnable();

        identifier = "FireRate";

        projectileType = "CandyCorn";

        rawDamage = GameData.Instance.rawDamageFireRate;
        rawFireRate = GameData.Instance.rawFireRateFireRate;
        rawRange = GameData.Instance.rawRangeFireRate;

        damage = rawDamage;
        fireRate = rawFireRate;
        range = rawRange;
    }

    protected override void ShootProjectile()
    {
        SoundManager.Instance.PlayShotFireRate();

        base.ShootProjectile();
    }

    protected override void Update()
    {
        base.Update();
    }
}