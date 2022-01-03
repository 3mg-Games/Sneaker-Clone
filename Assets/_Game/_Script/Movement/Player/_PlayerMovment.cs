using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneaker.Movement
{
    public class _PlayerMovment : MonoBehaviour
    {
        private CharacterController controller;
        public Animator anim;
        public Transform GroundChecker;
        public Joystick joystick;

        public float speed;
        public float rotationSmooth;

        public LayerMask GroundMask;

        float gravity;
        Vector3 velocity;
        
        public bool isGrounded;
        public bool isHoldingCloths;
        private float turnSmoothVelocity;
               
        private void Start()
        {
            controller = GetComponent<CharacterController>();
            gravity = Physics.gravity.y;
        }

        [HideInInspector] public Vector3 direction;
        private void Update()
        {
            movement();

            if (GetComponent<Sneaker.Core.PlayerStackingAndUnstacking>().ClothObject.Count > 0)
                isHoldingCloths = true;
            if (GetComponent<Sneaker.Core.PlayerStackingAndUnstacking>().ClothObject.Count <= 0)
                isHoldingCloths = false;

            anim.SetBool("hold", isHoldingCloths);


        }
        void movement()
        {
            float z = joystick.Vertical;
            float x = joystick.Horizontal;

            z = Input.GetAxisRaw("Vertical");
            x = Input.GetAxisRaw("Horizontal");
            direction = new Vector3(x, 0, z).normalized;
            anim.SetFloat("speed", Mathf.Abs(direction.magnitude));
            if (direction.magnitude > 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmooth);
                transform.rotation = Quaternion.Euler(0, angle, 0);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }

            isGrounded = Physics.CheckSphere(GroundChecker.position, 0.2f, GroundMask);

            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);


        }
    }
}
