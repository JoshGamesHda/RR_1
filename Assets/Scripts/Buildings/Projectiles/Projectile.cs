using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : MonoBehaviour
{
    public string identifier { get; protected set; }

    [SerializeField] protected GameObject particles;

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
            ReturnProjectile(0);
        }
    }
    protected void ReturnProjectile(float particleY)
    {
        GameObject particleSystem = Instantiate(particles);
        particleSystem.transform.position = new Vector3(transform.position.x, particleY, transform.position.z);

        Destroy(particleSystem, 1f);

        ProjectilePool.Instance.ReturnProjectile(gameObject);
    }
}