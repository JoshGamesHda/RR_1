using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;
using static UnityEngine.UI.Image;

public class LemonSeed : Projectile
{

    private void OnEnable()
    {
        identifier = "CandyCorn";

        behaviour = new Trajectory();

        speed = GameData.Instance.CandyCornProjSpeed;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_MOUNTAIN))
        {
            other.gameObject.GetComponent<Mountain>().DamageMountain(damage);

            ReturnProjectile();
        }
        if (other.CompareTag(Constants.TAG_GROUND)) ReturnProjectile();
    }
}
