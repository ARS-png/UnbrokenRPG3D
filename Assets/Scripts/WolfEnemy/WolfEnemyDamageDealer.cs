using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WolfEnemyDamageDealer : MonoBehaviour, IEnemyDamageDealer
{
    bool canDealDamage;
    bool hasDealthDamage;

    [SerializeField] float attackRadius;
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
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius, layerMask);

            foreach (var hit in hitColliders)
            {
                if (hit.TryGetComponent(out HealthSystem health))
                {
                    health.TakeDamage(weaponDamage);
                    print("Damage dealt to: " + hit.name);
                    hasDealthDamage = true;
                    break;
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
        Gizmos.DrawSphere(transform.position, attackRadius);
    }


}
