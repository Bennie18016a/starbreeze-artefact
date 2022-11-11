using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using StealthAI;

namespace Boss
{
    public class MicCheck : MonoBehaviour
    {
        [Header("Mic Checking Settings")]
        [Tooltip("How often (in seconds) is there a mic check")]
        public int timeBetweenChecks;
        [Tooltip("How long does the play have to answer a mic check")]
        public int timeToAnswer;

        private float time;

        private void Update()
        {
            while (time < timeBetweenChecks) { time += 1 * Time.deltaTime; return; }
        }

        private void GetDeadPeople()
        {
            List<GameObject> deadPeople = new List<GameObject>();
            foreach (GameObject dead in GameObject.FindGameObjectsWithTag("Dead"))
            {
                deadPeople.Add(dead);
            }
        }
    }
}
