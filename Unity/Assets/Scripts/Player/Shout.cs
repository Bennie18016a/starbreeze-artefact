using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Civillian;

public class Shout : MonoBehaviour
{
    public float radius;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Shout");
            Shouting();
        }
    }

    private void Shouting()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length > 0)
        {
            foreach(Collider col in rangeChecks)
            {
                GameObject AI = col.gameObject;
                if (AI.transform.name != "Civ") continue;
                Transform target = AI.transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    AI.GetComponent<CivillianMovement>().Shouted();
                }
            }
        }

        Debug.Log("Shout affected " + rangeChecks.Length);
    }
}
