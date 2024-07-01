using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawBreaker : Projectile
{
    private float aoeRange;
    private void OnEnable()
    {
        identifier = "JawBreaker";

        behaviour = new Trajectory();

        aoeRange = GameData.Instance.AoeRadius;
        speed = GameData.Instance.AoeProjSpeed;
        remainingLifeTime = 10;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Constants.TAG_PROJECTILE) && !other.CompareTag(Constants.TAG_MOUNTAIN))
        { 
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Destroy(sphere.GetComponent<SphereCollider>());

            float scaleFactor = aoeRange;
            sphere.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

            sphere.transform.position = transform.position;
            sphere.GetComponent<Renderer>().material.color = Color.red;

            Destroy(sphere, 0.2f);


            Collider[] hitColliders = Physics.OverlapSphere(transform.position, aoeRange);

            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag(Constants.TAG_ENEMY))
                {
                    collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                }
            }

            ReturnProjectile();
        }
    }
}
