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

        [Header("Fundamentals")]
        [Tooltip("The gameobject for boss text")]
        public GameObject bossText;

        private float time;
        private bool started;

        public List<GameObject> deadPeople = new List<GameObject>();

        private void Update()
        {
            while (time < timeBetweenChecks) { time += 1 * Time.deltaTime; return; }
            GetDeadPeople();
        }

        private void GetDeadPeople()
        {
            if (started) { return; }
            started = true;

            foreach (GameObject dead in GameObject.FindGameObjectsWithTag("Dead"))
            {
                deadPeople.Add(dead);
            }
            StartCoroutine(StartPagers());
        }

        private IEnumerator StartPagers()
        {
            foreach (GameObject dead in deadPeople)
            {
                bossText.SetActive(true);
                bossText.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = string.Format("{0}, are you there?", dead.name);
                dead.GetComponent<Pager>().StartPager(timeToAnswer);
                yield return new WaitForSeconds(timeToAnswer);
                bossText.SetActive(false);
                if (dead.GetComponent<Pager>().Answering())
                {
                    yield return new WaitForSeconds(timeToAnswer);
                }
                if (!dead.GetComponent<Pager>().PagerAnswered()) { break; }
                yield return new WaitForSeconds(4);
            }
            time = 0;
            started = return;
        }
    }
}
