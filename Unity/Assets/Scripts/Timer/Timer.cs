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
        string text = string.Format("{0}:{1}", minutes, (int)seconds);
        timer.text = text;


        if(seconds >= 60) { minutes += 1; seconds = 0; }
        seconds += 1 * Time.deltaTime;
    }
}
