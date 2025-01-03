using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingDurian : Enemy
{
    private void Awake()
    {
        healthMeter.InitializeBar(GameData.Instance.HPDurian);
    }
    protected override void OnEnable()
    {

        identifier = "KingDurian";

        base.OnEnable();

        maxHp = GameData.Instance.HPDurian;
        hp = maxHp;
        attackDamage = GameData.Instance.AttackDamageDurian;
        attackRange = GameData.Instance.AttackRangeDurian;
        moveSpeed = GameData.Instance.MoveSpeedDurian + GameData.Instance.MoveSpeedOffsetDurian;

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
