using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    #region Fields
    [SerializeField] protected Transform healthBarPosition;
    [SerializeField] protected Transform healthBar;
    [SerializeField] protected Transform healthMeter;
    private Vector3 originalHealthScale;


    protected string identifier;
    protected float maxHp, hp, attackDamage, attackRate, attackRange, moveSpeed;

    private GameObject destination;
    private Rigidbody rb;
    protected Animator animator;  // Reference to the Animator

    protected float nextTimeToAttack;
    protected bool attacking;
    #endregion
    
    private void Awake()
    {
        originalHealthScale = healthMeter.localScale;
    }
    protected virtual void OnEnable()
    {
        destination = GameManager.Instance.mountain;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        nextTimeToAttack = 0f;
    }

    protected virtual void FixedUpdate()
    {
        Movement();
        
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }

        UpdateHealthBar();
    }

    protected void Movement()
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
                if (animator != null) animator.SetTrigger("Attack");
            }
        }

        // If not close enough, keep moving
        if (!closeEnough)
        {
            Vector3 moveDirection = (destination.transform.position - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        }

        // If close enough, attack
        else 
        {
            Attack();
        }
    }
    protected virtual void Attack()
    {
        if (!attacking)
        {
            nextTimeToAttack = Time.time + 1f / attackRate;
            attacking = true;
        }
        if (Time.time >= nextTimeToAttack && attacking)
        {
            nextTimeToAttack = Time.time + 1f / attackRate;

            GameManager.Instance.mountain.GetComponent<Mountain>().DamageMountain(attackDamage);
        }
    }

    protected void UpdateHealthBar()
    {
        float hpRatio = hp / maxHp;

        healthMeter.localScale = new Vector3(hpRatio * originalHealthScale.x, originalHealthScale.y, healthBar.localScale.z);
    }
    public void ResetHealthBarScale()
    {
        healthMeter.localScale = originalHealthScale;
    }
    
    protected virtual void Die()
    {
        EnemyPool.Instance.ReturnEnemy(identifier, gameObject);
    }
}