using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    protected string identifier;
    protected float hp, attackDamage, attackRate, attackRange, moveSpeed;

    private GameObject destination;
    private Rigidbody rb;

    private float nextTimeToAttack;
    
    protected virtual void OnEnable()
    {
        destination = GameManager.Instance.mountain;
        rb = GetComponent<Rigidbody>();
        nextTimeToAttack = 0f;
    }

    protected virtual void FixedUpdate()
    {
        // Look at mountain
        Quaternion lookDirection = Quaternion.LookRotation(destination.transform.position - transform.position);
        transform.rotation = lookDirection;

        // In attack range?
        bool closeEnough = false;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag(Constants.TAG_MOUNTAIN))
            {
                closeEnough = true;
            }
        }

        // If not, START MOVING

        if (!closeEnough)
        {
            Vector3 moveDirection = (destination.transform.position - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        }

        // If close enough, ATTACK
        if (closeEnough && Time.time >= nextTimeToAttack)
        {
            Attack();
            nextTimeToAttack = Time.time + 1f / attackRate;
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            EnemyPool.Instance.ReturnEnemy(identifier, gameObject);
        }
    }

    protected virtual void Attack()
    {
        // Implement attack logic here
        GameManager.Instance.mountain.GetComponent<Mountain>().DamageMountain(attackDamage);
    }
}


public class Blueberry : Enemy
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void Attack()
    {
        base.Attack();
    }
}