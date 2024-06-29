using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : Enemy
{
    protected override void OnEnable()
    {
        identifier = "Strawberry";

        base.OnEnable();

        hp = GameData.HPStrawberry;
        attackDamage = GameData.AttackDamageStrawberry;
        attackRate = GameData.AttackRateStrawberry;
        attackRange = GameData.AttackRangeStrawberry;
        moveSpeed = GameData.MoveSpeedStrawberry;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Attack()
    {
        base.Attack();
    }
}
