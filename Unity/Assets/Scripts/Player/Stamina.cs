using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Stamina : MonoBehaviour
    {
        private Movement _movement;
        private int _stamina;

        private float _regenCool = 0.1f, _regenTime;
        private float _sprintCool = 0.05f, _sprintTime;
        private float _coolCool = 10, _coolTime;

        [HideInInspector] public bool canRun, runCool;

        [Header("Important Stamina Fundamentals")]
        [Tooltip("The maximum amount of stamina you have")]
        public int maxStamina;

        private void Start()
        {
            _movement = GetComponent<Movement>();
            _stamina = maxStamina;
        }

        private void Update()
        {
            canRun = _stamina > 0 && !runCool;

            Idle();
            Running();
            Cooldown();

            _regenTime += 1 * Time.deltaTime;
            _sprintTime += 1 * Time.deltaTime;
        }

        private void Cooldown()
        {
            if (runCool)
            {
                _coolTime += 1 * Time.deltaTime;
            }

            if (_coolTime >= _coolCool && runCool)
            {
                runCool = false;
                _coolTime = 0;
            }

            if (_stamina == 0)
            {
                runCool = true;
            }
        }

        private void Running()
        {
            if (_movement.running && canRun && _sprintTime > _sprintCool)
            {
                _stamina -= 1;
                _sprintTime = 0;
            }
        }

        private void Idle()
        {
            if (_stamina < maxStamina && _regenTime > _regenCool && !_movement.running)
            {
                _stamina += 1;
                _regenTime = 0;
            }
        }
    }
}