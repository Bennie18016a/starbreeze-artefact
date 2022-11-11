using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthAI;

namespace StealthAI
{
    public class FieldOfView : MonoBehaviour
    {
        public float radius;
        [HideInInspector] public float angle;
        [Range(0, 360)] public float idleAngle;
        [Range(0, 360)] public float chaseAngle;

        public GameObject playerRef;

        public LayerMask targetMask;
        public LayerMask aitargetMask;
        public LayerMask obstructionMask;

        public SusMeter susMeter;

        public bool canSeePlayer;

        private void Start()
        {
            playerRef = GameObject.FindWithTag("Player");
            StartCoroutine(FOVRoutine());
        }

        private void Update()
        {
            if (canSeePlayer) { angle = chaseAngle; }
            else { angle = idleAngle; }
        }

        private IEnumerator FOVRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(0.2f);

            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
                FieldOfViewCheckAI();
            }
        }

        private void FieldOfViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                        canSeePlayer = true;
                    else
                        canSeePlayer = false;
                }
                else
                    canSeePlayer = false;
            }
            else if (canSeePlayer)
                canSeePlayer = false;
        }

        private void FieldOfViewCheckAI()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, aitargetMask);

            foreach (Collider AI in rangeChecks)
            {
                GameObject AIobj = AI.gameObject;
                SusMeter AIsus = AIobj.GetComponent<SusMeter>();

                if (AIobj == gameObject) { return; }

                if (AIsus == null) { continue; }
                Transform target = AIobj.transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                        if (AIsus.aggro) { susMeter.value = 101; }
                }
            }
        }
    }
}
