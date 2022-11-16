using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    float seconds, minutes;
    public TMPro.TMP_Text timer;

    private void Update()
    {
        string _secs = ((int)seconds).ToString();
        string _mins = minutes.ToString();

        if (seconds >= 60) { minutes += 1; seconds = 0; }
        seconds += 1 * Time.deltaTime;

        string text;
        if(minutes > 10 && seconds > 10)
        {
           text = string.Format("{0}:{1}", _mins, _secs);
        } else if (minutes > 10)
        {
           text = string.Format("{0}:0{1}", _mins, _secs);
        } else if (seconds > 10)
        {
            text = string.Format("0{0}:{1}", _mins, _secs);
        }
        else
        {
            text = string.Format("0{0}:0{1}", _mins, _secs);
        }
        timer.text = text;
    }
}
