using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : SupportBuilding
{
    protected override void OnEnable()
    {
        base.OnEnable();

        identifier = "SpeedUp";

        buttonData.buildingName = "Sugar Factory";
        buttonData.buildingDescription = "Accelerates a tower's rate of fire";

        effect = new EffectSpeedUp();

        color = Color.yellow;

        AddBlock(new Block(-1, 0, this));
        AddBlock(new Block(0, 0, this));
        AddBlock(new Block(0, 1, this));
        AddBlock(new Block(0, -1, this));

        buttonData.buildingType = this;
    }
}