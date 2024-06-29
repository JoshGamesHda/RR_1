using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;
using static UnityEngine.UI.Image;

public class LemonSeed : MonoBehaviour
{
    private Vector3 origin, target;
    private float damage;
    private float projSpeed;
    private float launchTime;
    private float remainingLifeTime;


    IProjectileBehaviour projectile;

    void OnEnable()
    {
        projSpeed = GameData.ProjSpeedLemon;
        projectile = new StraightShot();

        launchTime = Time.time;
    }

    public void SetValues(Vector3 origin_, Vector3 target_, float damage_)
    {
        origin = origin_;
        transform.position = origin;
        target = target_;
        damage = damage_;

        remainingLifeTime = GameData.projectileLifetime;
    }

    void Update()
    {
        projectile.UpdateTrajectory(gameObject, origin, target, projSpeed, launchTime);

        remainingLifeTime -= Time.deltaTime;
        if (remainingLifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_MOUNTAIN))
        {
            GameManager.Instance.mountain.GetComponent<Mountain>().DamageMountain(damage);

            Destroy(gameObject);
        }
    }

}
