using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemon : Enemy
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shootPos;

    protected override void OnEnable()
    {
        identifier = "Lemon";

        base.OnEnable();

        maxHp = GameData.Instance.HPLemon;
        hp = maxHp;
        attackDamage = GameData.Instance.AttackDamageLemon;
        attackRate = GameData.Instance.AttackRateLemon;
        attackRange = GameData.Instance.AttackRangeLemon;
        moveSpeed = GameData.Instance.MoveSpeedLemon;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Attack()
    {
        ShootProjectile(); // Call base Attack method to trigger general attack behavior
        animator.SetTrigger("Attack"); // Trigger the "Attack" animation parameter
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