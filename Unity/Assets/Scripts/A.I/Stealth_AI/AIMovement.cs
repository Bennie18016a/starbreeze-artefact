using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthAI
{
    public class AIMovement : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] DrawWaypoints patrolPath;
        [Min(1.1f)]
        [SerializeField] float waypointTolerance = 1.1f;
    }
}
