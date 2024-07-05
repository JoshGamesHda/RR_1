using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUp : SupportBuilding
{
    protected override void OnEnable()
    {
        base.OnEnable();

        identifier = "RangeUp";

        effectStrength = GameData.Instance.rangeUp_Multiplier;

        effect = new EffectRangeUp();

        color = Color.red;

        AddBlock(new Block(-1, 0, this));
        AddBlock(new Block(0, 0, this));
        AddBlock(new Block(0, -1, this));
        AddBlock(new Block(1, -1, this));
    }
}
