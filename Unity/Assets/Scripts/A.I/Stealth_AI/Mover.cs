using Unity.VisualScripting;
using UnityEngine;
using StealthAI;

namespace AI
{
    public class Mover : MonoBehaviour
    {
        UnityEngine.AI.NavMeshAgent navMeshAgent;
        private SusMeter susMeter;
        private bool seen;
        public bool rotate;
        private Transform target;
        private void Start()
        {
            navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            susMeter = GetComponent<SusMeter>();

            if (rotate) return;
            navMeshAgent.updateRotation = false;
        }

        private void Update()
        {
            if (seen) { MoveTo(target.position); }
        }

        public void StartMoveAction(Vector3 destination)
        {
            MoveTo(destination);
        }

        public void Chase(Transform _target)
        {
            if (!susMeter.aggro) return;
            seen = true;
            target = _target;
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