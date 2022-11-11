using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

namespace StealthAI
{
    public class SusMeter : MonoBehaviour
    {
        #region Public Variables

        [Header("Fundamentals")]
        [Tooltip("The field of view script attached to the AI")]
        public FieldOfView fov;
        [Tooltip("Spotted Gameobject")]
        public GameObject spotted;
        [Tooltip("Sus Meter Gameobject")]
        public GameObject susMeterOBJ;

        [Header("Fundamental UI")]
        [Tooltip("The image that shows the meter")]
        public UnityEngine.UI.Image susMeter;
        [Tooltip("The text that shows how much they see you")]
        public TMPro.TMP_Text susText;
        [Tooltip("The name of the AI")]
        public RectTransform nameText;

        [Header("Sus Meter Settings")]
        [Tooltip("The speed they detect you at")]
        public int multipler = 1;
        [Tooltip("How close behind do they have to be for the player to start getting detected")]
        public float behind = 1.5f;

        #endregion

        #region Private Variables

        float value;
        float maxValue = 100;
        bool sawPlayer;
        [HideInInspector] public bool aggro;

        #endregion

        #region Start/Update
        private void Start()
        {
            susMeter.fillAmount = 0;
        }

        private void Update()
        {
            ValueSlider();
            ValueManager();
            UI();

            if (sawPlayer) { Spotted(); }
        }

        #endregion

        #region Functions

        private void ValueSlider()
        {
            if (fov.canSeePlayer || Behind())
            {
                value += 1 * multipler * Time.deltaTime;
            }
            else
            {
                value -= 1 * multipler * Time.deltaTime;
            }
        }
        private void UI()
        {
            susText.enabled = value > 0;
            susText.text = ((int)value).ToString();
            susMeter.fillAmount = value / 100;

            if (aggro)
            {
                nameText.localPosition = new Vector3(0, -1.25f, 0);
            }
            else if(value > 0)
            {
                nameText.localPosition = new Vector3(0, -0.5f, 0);
            } else
            {
                nameText.localPosition = new Vector3(0, -1.25f, 0);
            }
        }

        private void ValueManager()
        {
            if (value >= maxValue)
            {
                value = maxValue;
                sawPlayer = true;
            }

            if (value < 0)
            {
                value = 0;
            }
        }

        private bool Behind()
        {
            float distance = Vector3.Distance(fov.playerRef.transform.position, transform.position);
            return distance < behind;
        }

        private void Spotted()
        {
            aggro = true;
            spotted.SetActive(true);
            susMeterOBJ.SetActive(false);
        }

        #endregion

        #region Collisions

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Player"))
            {
                value = 100;
            }
        }
        #endregion
    }
}
