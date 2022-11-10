using Unity.VisualScripting;
using UnityEngine;

namespace StealthAI
{
    public class Mover : MonoBehaviour
    {
        UnityEngine.AI.NavMeshAgent navMeshAgent;
        private FieldOfView fov;
        public bool rotate;
        private void Start()
        {
            navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            fov = GetComponent<FieldOfView>();

            if (rotate) return;
            navMeshAgent.updateRotation = false;
        }

        public void StartMoveAction(Vector3 destination)
        {
            MoveTo(destination);
        }

        public void Chase(Transform target)
        {
            if (!fov.canSeePlayer) return;
            MoveTo(target.position);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
    }
}