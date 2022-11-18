using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using StealthAI;

public class Dead : MonoBehaviour
{
    [Header("Fundamentals")]
    [Tooltip("Is this a camera?")]
    public bool Camera;
    [Tooltip("Where must they walk to investigate")]
    public Transform InvestergationArea;

    private GameObject AI;
    private GameObject investGuard;

    private void Start()
    {
        if (!Camera) return;
        List<GameObject> Guards = new List<GameObject>();

        foreach(GameObject guard in GameObject.FindGameObjectsWithTag("AI"))
        {
            if(guard.name.ToLower() == "guard")
            {
                Guards.Add(guard);
            }
        }

        investGuard = Guards[Random.Range(0, Guards.Count)];
        investGuard.GetComponent<Mover>().StartMoveAction(InvestergationArea.position);
        investGuard.GetComponent<AIMovement>().enabled = false;
        Debug.Log(investGuard.transform.name);
    }

    private void Update()
    {
        if(Vector3.Distance(investGuard.transform.position, InvestergationArea.position) < 1.5f && Camera)
        {
            investGuard.GetComponent<AIMovement>().enabled = true;
        }
    }
}
