using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enviroment;

namespace AI
{
    public class Pager : MonoBehaviour
    {
        float time;
        int maxTime;
        bool answered;
        public bool started;
        bool stopped;

        public Interaction inter;
        public Outline outline;
        [Tooltip("The gameobject for player text")]
        public GameObject playerText;

        private void Update()
        {
            if (started && !inter._holding) { time += 1 * Time.deltaTime; }
            if (time > maxTime) { StopPager(); }
            inter.enabled = started;
            outline.enabled = started;

            if (inter._holding) { time = -5; }
            if (!inter._holding && time < 0 && !stopped) { StopPager(); }
        }

        public void StartPager(int time)
        {
            started = true;
            stopped = false;
            maxTime = time;
            time = 0;
        }

        public void StopPager()
        {
            stopped = true;
            inter.textOBJ.text = null;
            inter.enabled = false;
            started = false;
            outline.enabled = false;

            if (!PagerAnswered())
            {
                Debug.Log("Alarm");
            }
        }

        public bool PagerAnswered()
        {
            return answered;
        }

        public void AnsweredPager()
        {
            StartCoroutine(AnswerPager());
        }

        private IEnumerator AnswerPager()
        {
            answered = true;
            StopPager();
            playerText.SetActive(true);
            playerText.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = string.Format("{0} here, everything seems fine. Out", gameObject.name);
            yield return new WaitForSeconds(4);
            playerText.SetActive(false);
        }

        public bool Answering()
        {
            return inter._holding;
        }
    }
}
