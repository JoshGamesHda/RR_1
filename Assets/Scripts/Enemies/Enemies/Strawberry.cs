using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : Enemy
{
    private void Awake()
    {
        healthMeter.InitializeBar(GameData.Instance.HPStrawberry);
    }
    protected override void OnEnable()
    {
        identifier = "Strawberry";

        base.OnEnable();

        maxHp = GameData.Instance.HPStrawberry;
        hp = maxHp;
        attackDamage = GameData.Instance.AttackDamageStrawberry;
        attackRange = GameData.Instance.AttackRangeStrawberry;
        moveSpeed = GameData.Instance.MoveSpeedStrawberry;

        healthMeter.ResetValue();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void Die()
    {
        base.Die();
    }
}
