using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueberry : Enemy
{
    private void Awake()
    {
        healthMeter.InitializeBar(GameData.Instance.HPBlueberry);
    }
    protected override void OnEnable()
    {
        identifier = "Blueberry";

        base.OnEnable();

        maxHp = GameData.Instance.HPBlueberry;
        hp = maxHp;
        attackDamage = GameData.Instance.AttackDamageBlueberry;
        attackRange = GameData.Instance.AttackRangeBlueberry;
        moveSpeed = GameData.Instance.MoveSpeedBlueberry;

        healthMeter.ResetValue();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag(Constants.TAG_MOUNTAIN))
            {
                Attack();
            }
        }
    }

    protected override void Attack()
    {
        GameManager.Instance.mountain.GetComponent<Mountain>().DamageMountain(attackDamage);
        Die();
    }

    protected override void Die()
    {
        base.Die();
    }
}
