using UnityEngine;
using System;

public class CircleDetector : MonoBehaviour
{
    [SerializeField] float radius;

    bool wasInZoneLastFrame = false; 
    int layerMask = 1 << 15;
    Color gizmoColor = Color.green;

    public event Action OnPlayerDetected;

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        bool isInZoneNow = hitColliders.Length > 0;


        if (isInZoneNow && !wasInZoneLastFrame)
        {
            EnableDetector();
        }

 
        wasInZoneLastFrame = isInZoneNow;
 
        gizmoColor = isInZoneNow ? Color.red : Color.green;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(this.transform.position, radius);
    }

    public void EnableDetector()
    {
        Debug.Log("Player Enter is Sphere");

        GameEventsManager.instance.questStepPrefabsEvents.DetectPlayer(/*questId*/);
    }

}