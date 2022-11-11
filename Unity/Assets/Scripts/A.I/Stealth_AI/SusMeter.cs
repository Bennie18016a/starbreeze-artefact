using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthAI
{
    public class SusMeter : MonoBehaviour
    {
        #region Public Variables

        [Header("Fundamentals")]
        [Tooltip("The field of view script attached to the AI")]
        public FieldOfView fov;
        [Tooltip("The image that shows the meter")]
        public UnityEngine.UI.Image susMeter;
        [Tooltip("The text that shows how much they see you")]
        public TMPro.TMP_Text susText;

        [Header("Sus Meter Settings")]
        [Tooltip("The speed they detect you at")]
        public int multipler = 1;

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
        }

        #endregion

        #region Functions

        private void ValueSlider()
        {
            if (fov.canSeePlayer)
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
        }

        private void ValueManager()
        {
            if (value >= maxValue)
            {
                value = maxValue;
                sawPlayer = true;
            }

            if (sawPlayer)
            {
                aggro = true;
            }

            if (value < 0)
            {
                value = 0;
            }
        }

        #endregion
    }
}
