using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : AttackTower
{
    protected override void OnEnable()
    {
        base.OnEnable();

        identifier = "AOE";

        buttonData.buildingName = "Jawbreaker Mortar";
        buttonData.buildingDescription = "Shoots massive exploding balls";

        buttonData.buildingType = this;

        projectileType = "JawBreaker";

        rawDamage = GameData.rawDamageAOE;
        rawFireRate = GameData.rawFireRateAOE;
        rawRange = GameData.rawRangeAOE;
    }

    protected override void Update()
    {
        base.Update();
    }
}