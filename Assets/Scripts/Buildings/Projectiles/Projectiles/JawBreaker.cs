using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawBreaker : Projectile
{
    private float aoeRange;
    private GameObject aoeBall;
    private float aoeBallLifeTime;
    private bool aoeBallCountdown;
    private void OnEnable()
    {
        identifier = "JawBreaker";

        behaviour = new Trajectory();

        aoeRange = GameData.AoeRadius;
        speed = GameData.AoeProjSpeed;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, aoeRange);

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag(Constants.TAG_ENEMY))
            {
                collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);

                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Destroy(sphere.GetComponent<SphereCollider>());

                float scaleFactor = aoeRange / 2;
                sphere.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                
                sphere.transform.position = transform.position;
                sphere.GetComponent<Renderer>().material.color = Color.red;

                Destroy(sphere, 0.1f);

                ReturnProjectile();
            }

            if (collider.CompareTag(Constants.TAG_GROUND))
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Destroy(sphere.GetComponent<SphereCollider>());

                float scaleFactor = aoeRange / 2;
                sphere.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

                sphere.transform.position = transform.position;
                sphere.GetComponent<Renderer>().material.color = Color.red;

                Destroy(sphere, 0.1f);

                ReturnProjectile();
            }
        }
    }
}
