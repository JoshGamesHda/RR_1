using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CandyCorn : Projectile
{
    private void OnEnable()
    {
        identifier = "CandyCorn";

        behaviour = new StraightShot();

        speed = GameData.Instance.CandyCornProjSpeed;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_ENEMY))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);

            ReturnProjectile(0.6f);
        }
        if (other.CompareTag(Constants.TAG_GROUND)) ReturnProjectile(-0.6f);
    }
}
