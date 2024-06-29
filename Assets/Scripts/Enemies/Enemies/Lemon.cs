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
        ShootProjectile();
    }

    private void ShootProjectile()
    {
        Debug.Log("shooting projectile");

        GameObject proj = Instantiate(projectile);

        proj.GetComponent<LemonSeed>().SetValues(shootPos.position, GameManager.Instance.mountain.transform.position, attackDamage);
    }
}
