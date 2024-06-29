using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CandySpike : Projectile
{
    private void OnEnable()
    {
        identifier = "CandySpike";

        behaviour = new StraightShot();

        speed = GameData.CandySpikeProjSpeed;
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
