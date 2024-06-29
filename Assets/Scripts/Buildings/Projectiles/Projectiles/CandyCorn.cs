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

        speed = GameData.CandyCornProjSpeed;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(Constants.TAG_ENEMY))
        {
            collision.collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);

            ReturnProjectile();
        }
    }
}
