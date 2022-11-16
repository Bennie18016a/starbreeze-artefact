using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    [Header("Fundamentals")]
    [Tooltip("Is this a camera?")]
    public bool Camera;
    [Tooltip("Where must they walk to investigate")]
    public Transform InvestergationArea;

    private GameObject AI;

    private void Start()
    {
        List<GameObject> Guards = new List<GameObject>();

        foreach(GameObject guard in FindGameObjectsWithTag("AI"))
        {
            if(guard.name.ToLower() == "guard")
            {
                Guards.Add(guard);
            }
        }
    }
}
