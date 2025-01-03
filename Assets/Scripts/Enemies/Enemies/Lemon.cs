using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemon : Enemy
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shootPos;

    private void Awake()
    {
        healthMeter.InitializeBar(GameData.Instance.HPLemon);
    }
    protected override void OnEnable()
    {
        identifier = "Lemon";

        base.OnEnable();

        maxHp = GameData.Instance.HPLemon;
        hp = maxHp;
        attackDamage = GameData.Instance.AttackDamageLemon;
        attackRange = GameData.Instance.AttackRangeLemon;
        moveSpeed = GameData.Instance.MoveSpeedLemon + GameData.Instance.MoveSpeedOffsetLemon;

        healthMeter.ResetValue();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Attack()
    {
        ShootProjectile();
    }

    private void ShootProjectile()
    {
        GameObject proj = ProjectilePool.Instance.GetProjectile(Constants.ID_PROJECTILE_LEMONSEED);
        proj.GetComponent<LemonSeed>().SetValues(shootPos.position, GameManager.Instance.mountain.transform.position, attackDamage);
    }

    protected override void Die()
    {
        base.Die();
    }
}