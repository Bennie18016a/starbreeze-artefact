using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthAI;

public class Alarm : MonoBehaviour
{
    public void AlarmFunc()
    {
        Debug.Log("alarm");
        foreach(GameObject AI in GameObject.FindGameObjectsWithTag("AI"))
        {
            AI.GetComponent<SusMeter>().value = 101;
        }
    }
}
