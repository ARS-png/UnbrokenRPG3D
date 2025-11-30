using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDamageDealer : MonoBehaviour, IEnemyDamageDealer
{
    bool canDealDamage;
    bool hasDealthDamage;

    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;

    int layerMask = 1 << 15;

    private void Start()
    {
        canDealDamage = false;
        hasDealthDamage = false;
    }

    private void Update()
    {
        if (canDealDamage && !hasDealthDamage)
        {
            RaycastHit hit;

     

            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            {
                if (hit.transform.TryGetComponent(out HealthSystem health))
                {
                    health.TakeDamage(weaponDamage);
                    print("Enemy has dealth damage");
                    hasDealthDamage = true;
                }           
            }
        }
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealthDamage = false;
    }

    public void EndDealDamage()
    {
        canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up* weaponLength);
    }
}
