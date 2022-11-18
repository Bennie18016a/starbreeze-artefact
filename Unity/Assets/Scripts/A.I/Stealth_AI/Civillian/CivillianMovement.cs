using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using StealthAI;

namespace Civillian
{
    public class CivillianMovement : MonoBehaviour
    {
        [SerializeField] DrawWaypoints patrolPath;
        [Min(1.1f)]
        [SerializeField] float waypointTolerance = 1.1f;
        [SerializeField] SusMeter susMeter;

        public List<GameObject> escapePoints = new List<GameObject>();
        GameObject escapePoint;

        Mover mover;
        GameObject player;

        Vector3 guardPosition;

        int currentWaypointIndex = 0;
        [HideInInspector] public bool scared;

        private void Start()
        {
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");
            escapePoint = escapePoints[Random.Range(0, escapePoints.Count)];
            guardPosition = transform.position;
        }

        private void Update()
        {
            if (susMeter.aggro && !scared)
            {
                mover.StartMoveAction(escapePoint.transform.position);
            }
            else if (scared)
            {
                mover.StartMoveAction(transform.position);
            } else if (!susMeter.aggro)
            {
                PatrolBehaviour();
            }

            if (Vector3.Distance(transform.position, escapePoint.transform.position) < 2.5) { Destroy(gameObject); }
        }

        public void Shouted()
        {
            scared = true;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }

            mover.StartMoveAction(nextPosition);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }
    }
}
