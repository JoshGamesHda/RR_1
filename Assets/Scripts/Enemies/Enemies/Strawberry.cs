using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : Enemy
{
    protected override void OnEnable()
    {
        identifier = "Strawberry";

        base.OnEnable();

        maxHp = GameData.Instance.HPStrawberry;
        hp = maxHp;
        attackDamage = GameData.Instance.AttackDamageStrawberry;
        attackRate = GameData.Instance.AttackRateStrawberry;
        attackRange = GameData.Instance.AttackRangeStrawberry;
        moveSpeed = GameData.Instance.MoveSpeedStrawberry;
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
