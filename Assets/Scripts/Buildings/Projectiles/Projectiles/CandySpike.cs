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

        speed = GameData.Instance.CandySpikeProjSpeed;
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

            ReturnProjectile(-0.6f);
        }
    }

}
