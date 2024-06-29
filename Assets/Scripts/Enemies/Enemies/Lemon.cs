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

        hp = GameData.HPLemon;
        attackDamage = GameData.AttackDamageLemon;
        attackRate = GameData.AttackRateLemon;
        attackRange = GameData.AttackRangeLemon;
        moveSpeed = GameData.MoveSpeedLemon;
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
        GameObject proj = Instantiate(projectile);

        proj.GetComponent<LemonSeed>().SetValues(shootPos.position, GameManager.Instance.mountain.transform.position, attackDamage);
    }
}
