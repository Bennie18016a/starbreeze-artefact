using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmScr : MonoBehaviour
{
    private bool alarm, alarmed;
    public float time, maxtime;
    public Alarm alr;

    private void Update()
    {
        if (alarm)
        {
            time += 1 * Time.deltaTime;
        }

        if(time > maxtime && !alarmed)
        {
            Debug.Log(alarm);
            alarmed = true;
            alr.AlarmFunc();
        }
    }

    public void SoundAlarm()
    {
        alarm = true;
    }
}
