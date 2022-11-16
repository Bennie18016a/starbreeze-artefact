using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [Header("Speed and Gravity")]
        public float speed = 7.5f;
        public float sprint = 9.0f;
        public float crawl = 2.0f;
        public float jumpSpeed = 8.0f;
        public float gravity = 20.0f;

        [Header("Camera Settings")]
        public Camera playerCamera;
        public float lookSpeed = 2.0f;
        public float lookXLimit = 50.0f;

        [Header("Leaning")]
        public Transform leanPivot;
        private float currentLean, targetLean, leanVelocity;
        public float leanAngle, leanSmoothing;
        private bool leanLeft, leanRight;

        private Vector3 Stand;
        private Vector3 Crouch;

        [HideInInspector] public bool running;
        [HideInInspector] public bool crouching;

        CharacterController characterController;
        [HideInInspector] public Vector3 moveDirection = Vector3.zero;
        Vector2 rotation = Vector2.zero;

        [HideInInspector] public bool canMove = true;

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            rotation.y = transform.eulerAngles.y;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Stand = new Vector3(1, 1, 1);
            Crouch = new Vector3(1, 0.5f, 1);
        }

        void Update()
        {
            PlayerMove();
            CameraMove();
            CalculateLeaning();
            leanLeft = Input.GetKey(KeyCode.Q);
            leanRight = Input.GetKey(KeyCode.E);
        }

        #region ~ Movement & Camera ~

        private void CameraMove()
        {

            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);

        }

        private void PlayerMove()
        {
            if (characterController.isGrounded && canMove)
            {
                crouching = Input.GetKey(KeyCode.LeftControl);
                running = !crouching && Input.GetKey(KeyCode.LeftShift) && GetComponent<Stamina>().canRun;

                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Vector3 right = transform.TransformDirection(Vector3.right);
                float curSpeedX = (running ? sprint : crouching ? crawl : speed) * Input.GetAxis("Vertical");
                float curSpeedY = (running ? sprint : crouching ? crawl : speed) * Input.GetAxis("Horizontal");
                moveDirection = (forward * curSpeedX) + (right * curSpeedY);

                if (Input.GetButton("Jump") && canMove)
                {
                    moveDirection.y = jumpSpeed;
                }

                if (crouching)
                {
                    gameObject.transform.localScale = Crouch;
                }
                else
                {
                    gameObject.transform.localScale = Stand;
                }
            }

            moveDirection.y -= gravity * Time.deltaTime;

            characterController.Move(moveDirection * Time.deltaTime);
        }
        #endregion

        #region  ~ Leaning ~

        private void CalculateLeaning()
        {
            if (leanLeft)
            {
                targetLean = leanAngle;
            }
            else if (leanRight)
            {
                targetLean = -leanAngle;
            }
            else
            {
                targetLean = 0;
            }
            currentLean = Mathf.SmoothDamp(currentLean, targetLean, ref leanVelocity, leanSmoothing);

            leanPivot.localRotation = Quaternion.Euler(new Vector3(0, 0, currentLean));
        }
        #endregion
    }
}