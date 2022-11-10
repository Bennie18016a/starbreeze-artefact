using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        bool _holding;

        private void Start()
        {
            textOBJ.gameObject.transform.name = string.Format("{0}'s text OBJ", gameObject.transform.name);
            _fillMultipler = time * 0.01f;
            _fillMultipler *= 4;
        }

        private void Update()
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < dist)
            {
                textOBJ.enabled = true;
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
                textOBJ.enabled = false;
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
            Destroy(textOBJ.gameObject);
            interactionCircle.fillAmount = 0;

            if ((int)Modes == 0)
            {
                GetComponent<Animation>().Play();
            }

            this.enabled = false;
        }
    }
}