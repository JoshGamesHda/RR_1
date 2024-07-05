using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : SupportBuilding
{
    protected override void OnEnable()
    {
        base.OnEnable();

        identifier = "DamageUp";

        effect = new EffectDamageUp();

        color = Color.red + Color.blue;

        AddBlock(new Block(-2, 0, this));
        AddBlock(new Block(-1, 0, this));
        AddBlock(new Block(0, 0, this));
        AddBlock(new Block(0, 1, this));
    }
}