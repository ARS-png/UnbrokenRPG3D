using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    private float health = 3;
    [SerializeField] GameObject ragdoll;

    Animator animator;

    private int levelCount;


    public static event System.Action OnPlayerDie;


    private void Start()
    {
        animator = GetComponent<Animator>();

        try
        {
            levelCount = FindFirstObjectByType<DifficultySaver>().countOfDifficultyLevels;
            health = levelCount - FindFirstObjectByType<DifficultySaver>().currentDifficulty;
            Debug.Log("heath in health system = " + health);
        }
        catch (Exception ex)
        {
            Debug.Log($"{ex.Message}");
        }



    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        animator.SetTrigger("damage");

        if (health <= 0)
        {
 

            Die();

          
        }
    }



    void Die()
    {

        OnPlayerDie?.Invoke();

        Instantiate(ragdoll, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    

    public float GetHealth()
    {
        return health;
    }
}
