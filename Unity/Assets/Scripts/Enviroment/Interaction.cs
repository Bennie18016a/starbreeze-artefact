using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Player;
using AI;

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

        public enum mode
        { Door, Pager }
        [Tooltip("What it does: door, lockpick, pager etc")]
        public mode Modes;

        [Header("Fundamentals")]
        [Tooltip("The player gameobject")]
        public GameObject player;
        [Tooltip("How far you can be from the obj")]
        public float dist;
        [Tooltip("Text object for interaction text")]
        public TMP_Text textOBJ;
        [Tooltip("String for input text")]
        public string text;
        [Tooltip("Image of interaction circle")]
        public Image interactionCircle;

        float _time;
        float _fillTime;
        float _fillMultipler;
        bool _holding, nulled = true;

        private void Start()
        {
            _fillMultipler = time * 0.01f;
            _fillMultipler *= 4;
        }

        private void Update()
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < dist)
            {
                nulled = false;
                textOBJ.text = text;
                if (hold) { _holding = Input.GetKey(key); }
                else if (Input.GetKeyDown(key))
                {
                    Finish();
                }

                if (_holding)
                {
                    player.GetComponent<Movement>().canMove = !_holding;
                }
                else
                {
                    player.GetComponent<Movement>().canMove = !_holding;
                    _fillTime = 0;
                }
            }
            else
            {
                if (!nulled)
                {
                    textOBJ.text = null;
                    nulled = true;
                }
            }

            interactionCircle.fillAmount = _fillTime;
            TimeManagement();
        }

        private void TimeManagement()
        {
            if (_time < time && !_holding) { _time = 0; }
            if (_time > time) { Finish(); }

            if (_holding) { _fillTime += _fillMultipler * Time.deltaTime; }
            _time += 1 * Time.deltaTime;
        }

        public void Finish()
        {
            Debug.Log("Succesfully Finished!");
            player.GetComponent<Movement>().canMove = true;
            textOBJ.text = null;
            interactionCircle.fillAmount = 0;

            if ((int)Modes == 0)
            {
                GetComponent<Animation>().Play();
            }

            if ((int)Modes == 1)
            {
                GetComponent<Pager>().AnsweredPager();
            }

            this.enabled = false;
        }
    }
}