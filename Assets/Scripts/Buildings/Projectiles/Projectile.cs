using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : MonoBehaviour
{
    public string identifier { get; protected set; }

    protected float speed;
    protected float damage;
    protected Vector3 origin, target;
    protected IProjectileBehaviour behaviour;

    private float launchTime;
    protected float remainingLifeTime;

    public void SetValues(Vector3 origin_, Vector3 target_, float damage_)
    {
        origin = origin_;
        transform.position = origin;
        target = target_;
        damage = damage_;

        launchTime = Time.time;
        remainingLifeTime = GameData.Instance.projectileLifetime;
    }
    protected virtual void Update()
    {
        behaviour.UpdateTrajectory(gameObject, origin, target, speed, launchTime);

        remainingLifeTime -= Time.deltaTime;
        if(remainingLifeTime < 0)
        {
            Debug.Log("Time ran out");
            ReturnProjectile();
        }
    }

    protected void ReturnProjectile()
    {
        ProjectilePool.Instance.ReturnProjectile(gameObject);
    }
}