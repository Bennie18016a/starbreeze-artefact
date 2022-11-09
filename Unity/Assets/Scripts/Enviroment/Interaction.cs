using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Player;

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
        [Tooltip("Text object for interaction text")]
        public TMP_Text textOBJ;
        [Tooltip("String for input text")]
        public string text;

        float _time;
        bool holding;

        private void Start()
        {
            textOBJ.gameObject.transform.name = string.Format("{0}'s text OBJ", gameObject.transform.name);
        }

        private void Update()
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if(distance < dist)
            {
                textOBJ.enabled = true;
                textOBJ.text = text;
                if(hold) { holding = Input.GetKey(key); }
                else if(Input.GetKeyDown(key))
                {
                    Finish();
                }
            } else 
            {
                textOBJ.enabled = false;
            }

            if(holding)
            {
                player.GetComponent<Movement>().canMove = !holding;
            }

            TimeManagement();
        }

        private void TimeManagement()
        {
            if(_time < time && !holding) { _time = 0; }
            if(_time > time) { Finish(); }

            _time += 1 * Time.deltaTime;
        }

        public void Finish()
        {
            Debug.Log("Succesfully Finished!");
            this.enabled = false;
            Destroy(textOBJ.gameObject);
            _time = 0;
        }
    }
}