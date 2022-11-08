using UnityEngine;

namespace Player
{
    public class Headbob : MonoBehaviour
    {
        [Header("Fundamentals")]
        public float walkingBobbingSpeed = 14.0f;
        public float sprintBobbingSpeed = 18.0f;
        public float bobbingAmount = 0.05f;
        public float sprintBobbingAmount = 0.1f;
        public Movement controller;

        float defaultPosY = 0;
        float timer = 0;

        void Start()
        {
            defaultPosY = transform.localPosition.y;
        }

        void Update()
        {
            if (Mathf.Abs(controller.moveDirection.x) > 0.1f || Mathf.Abs(controller.moveDirection.z) > 0.1f)
            {
                timer += Time.deltaTime * (controller.running ? sprintBobbingSpeed : walkingBobbingSpeed);
                transform.localPosition = new Vector3(transform.localPosition.x,
                    defaultPosY + Mathf.Sin(timer) * (controller.running ? sprintBobbingAmount : bobbingAmount), transform.localPosition.z);
            }
            else
            {
                timer = 0;
                transform.localPosition = new Vector3(transform.localPosition.x,
                    Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * (controller.running ? sprintBobbingSpeed : walkingBobbingSpeed)),
                    transform.localPosition.z);
            }
        }
    }
}