using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enviroment
{
    public class Interaction : MonoBehaviour
    {
        [Header("Interaction Settings")]
        [Tooltip("The key to hold for interaction")]
        public KeyCode key;
        [Tooltip("Do you need to hold the button?")]
        public bool hold;
        [Tooltip("How long to hold it")]
        public int time;

        [Header("Fundamentals")]
        [Tooltip("The player gameobject")]
        public GameObject player;
        [Tooltip("How far you can be from the obj")]
        public float dist;
        float _time;

        private void Update()
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if(distance < dist)
            {
                Debug.Log("In Reach");
            }
        }
    }
}