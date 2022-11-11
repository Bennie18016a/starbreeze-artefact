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
        bool started;

        public Interaction inter;

        private void Update()
        {
            while (started)
            {
                time += 1 * Time.deltaTime;
            }

            if (time > maxTime)
            {
                StopPager();
            }
            inter.enabled = started;
        }

        public void StartPager(int time)
        {
            maxTime = time;
            started = true;
        }

        public void StopPager()
        {
            started = false;

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
            answered = true;
        }
    }
}
