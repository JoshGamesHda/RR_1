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

        rawDamage = GameData.rawDamageFireRate;
        rawFireRate = GameData.rawFireRateFireRate;
        rawRange = GameData.rawRangeFireRate;
    }

    protected override void Update()
    {
        base.Update();
    }
}