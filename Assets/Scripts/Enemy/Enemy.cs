using NUnit.Framework.Interfaces;
using UnityEngine;

using System.Collections.Generic;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] GameObject ragdoll;

    [Header("Combat")]
    [SerializeField] float attackCD = 3f;//time between the atacks
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;



    GameObject player;
    Animator animator;
    NavMeshAgent agent;
    float timePassed = 0;
    float newDestinationCD = 0.5f;

    //new
    private EnemyHealthBar healthBar;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    private void Update()
    {
        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

        if (player == null)
        {
            return;
        }


        if (timePassed >= attackCD)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                animator.SetTrigger("attack");
                timePassed = 0;
            }
        }
        timePassed += Time.deltaTime;



        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position - new Vector3(1.5f, 0, 1.5f));
        }
        newDestinationCD -= Time.deltaTime;


        Vector3 targetPosition = player.transform.position;
        targetPosition.y = transform.position.y; // Игнорируем разницу по высоте
        transform.LookAt(targetPosition);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        healthBar.TakeDamage((int)damageAmount);


        animator.SetTrigger("damage");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(ragdoll, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }


    private void StartDealDamage()
    {
        GetComponentInChildren<IEnemyDamageDealer>()?.StartDealDamage();

    }

    private void EndDealDamage()
    {
        GetComponentInChildren<IEnemyDamageDealer>()?.EndDealDamage();
    }

    public float GetHealth()
    {
        return health;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

}
