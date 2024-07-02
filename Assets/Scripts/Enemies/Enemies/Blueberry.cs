using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueberry : Enemy
{
    protected override void OnEnable()
    {
        identifier = "Blueberry";

        base.OnEnable();

        maxHp = GameData.Instance.HPBlueberry;
        hp = maxHp;
        attackDamage = GameData.Instance.AttackDamageBlueberry;
        attackRate = GameData.Instance.AttackRateBlueberry;
        attackRange = GameData.Instance.AttackRangeBlueberry;
        moveSpeed = GameData.Instance.MoveSpeedBlueberry;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Attack()
    {
        base.Attack();
        Die();
    }

    protected override void Die()
    {
        base.Die();
    }
}
