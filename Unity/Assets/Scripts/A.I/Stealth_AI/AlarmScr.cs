using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Civillian;

public class AlarmScr : MonoBehaviour
{
    private bool alarm, alarmed;
    public float time, maxtime;
    public Alarm alr;

    private void Update()
    {
        if (transform.name == "Civ" && GetComponent<CivillianMovement>().scared) return;
        if (alarm)
        {
            time += 1 * Time.deltaTime;
            if (transform.name == "Civ" && GetComponent<CivillianMovement>().scared) { alarm = false; }
        }

        if(time > maxtime && !alarmed)
        {
            if (transform.name == "Civ" && GetComponent<CivillianMovement>().scared) return;
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
