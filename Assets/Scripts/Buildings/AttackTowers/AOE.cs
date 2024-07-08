using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : AttackTower
{
    protected override void OnEnable()
    {
        base.OnEnable();

        identifier = "AOE";

        projectileType = "JawBreaker";

        rawDamage = GameData.Instance.rawDamageAOE;
        rawFireRate = GameData.Instance.rawFireRateAOE;
        rawRange = GameData.Instance.rawRangeAOE;

        damage = rawDamage;
        fireRate = rawFireRate;
        range = rawRange;
    }

    protected override void ShootProjectile()
    {
        SoundManager.Instance.PlayShotAoe();

        GameObject projectile = ProjectilePool.Instance.GetProjectile(projectileType);

        if (projectile != null)
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y - 1f, target.transform.position.z);
            projectile.GetComponent<Projectile>().SetValues(transform.position + shootOffset, targetPosition, damage);
        }
    }

    protected override void Update()
    {
        base.Update();
    }
}