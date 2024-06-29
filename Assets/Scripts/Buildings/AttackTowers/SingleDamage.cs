using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDamage : AttackTower
{
    protected override void OnEnable()
    {
        base.OnEnable();

        identifier = "SingleDamage";

        buttonData.buildingName = "Candy Spike Ballista";
        buttonData.buildingDescription = "Extra spikey";
        
        buttonData.buildingType = this;

        projectileType = "CandySpike";

        rawDamage = GameData.Instance.rawDamageBallista;
        rawFireRate = GameData.Instance.rawFireRateBallista; 
        rawRange = GameData.Instance.rawRangeBallista;

        damage = rawDamage;
        fireRate = rawFireRate;
        range = rawRange;
    }

    protected override void Update()
    {
        base.Update();
    }
}