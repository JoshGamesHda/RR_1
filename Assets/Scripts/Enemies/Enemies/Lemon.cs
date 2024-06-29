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

        hp = GameData.Instance.HPLemon;
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
        Debug.Log("Attacking");
        ShootProjectile(); // Call base Attack method to trigger general attack behavior
        animator.SetTrigger("Attack"); // Trigger the "Attack" animation parameter
    }

    private void ShootProjectile()
    {
        GameObject proj = Instantiate(projectile);
        proj.GetComponent<LemonSeed>().SetValues(shootPos.position, GameManager.Instance.mountain.transform.position, attackDamage);
    }
}

