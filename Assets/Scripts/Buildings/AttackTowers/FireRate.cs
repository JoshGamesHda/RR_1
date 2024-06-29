using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRate : AttackTower
{
    protected override void OnEnable()
    {
        base.OnEnable();

        identifier = "FireRate";

        buttonData.buildingName = "Gatling Globe";
        buttonData.buildingDescription = "This guy shoots pretty fast";

        buttonData.buildingType = this;

        projectileType = "CandyCorn";

        rawDamage = GameData.Instance.rawDamageFireRate;
        rawFireRate = GameData.Instance.rawFireRateFireRate;
        rawRange = GameData.Instance.rawRangeFireRate;

        damage = rawDamage;
        fireRate = rawFireRate;
        range = rawRange;
    }

    protected override void Update()
    {
        base.Update();
    }
}