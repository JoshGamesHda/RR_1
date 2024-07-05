using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : AttackTower
{
    protected override void OnEnable()
    {
        base.OnEnable();

        identifier = "AOE";

        projectileType = "JawBreaker";

        rawDamage = GameData.Instance.rawDamageAOE;
        rawFireRate = GameData.Instance.rawFireRateAOE;
        rawRange = GameData.Instance.rawRangeAOE;

        damage = rawDamage;
        fireRate = rawFireRate;
        range = rawRange;
    }

    protected override void Update()
    {
        base.Update();
    }
}