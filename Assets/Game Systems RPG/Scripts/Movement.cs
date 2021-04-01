using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Debugging.Player
{
    [AddComponentMenu("RPG/Player/Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [Header("Speed Vars")]
        public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;
        private float _gravity = 20.0f;
        private Vector3 _moveDir;
        private CharacterController _charC;

        private Animator characterAnimator;


        private void Start()
        {
            _charC = GetComponent<CharacterController>();
            characterAnimator = GetComponentInChildren<Animator>();
        }
        private void Update()
        {
            Move();
        }
        private void Move()
        {
            Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (_charC.isGrounded)
            {

                if (Input.GetButton("Crouch"))
                {
                    moveSpeed = crouchSpeed;
                    characterAnimator.SetFloat("Speed", 0.25f);
                }
                else
                {
                    if (Input.GetButton("Sprint"))
                    {
                        moveSpeed = runSpeed;
                        characterAnimator.SetFloat("Speed", 3f);
                    }
                    else if (!Input.GetButton("Sprint"))
                    {
                        moveSpeed = walkSpeed;
                        characterAnimator.SetFloat("Speed", 1f);
                    }
                }

                characterAnimator.SetBool("Walking", movementInput.magnitude > 0.05f);

                //if(movementInput.magnitude > 0.05f)
                //{
                //    characterAnimator.SetBool("Walking", true);
                //}
                //else
                //{
                //    characterAnimator.SetBool("Walking", false);
                //}

                
                _moveDir = transform.TransformDirection(new Vector3(movementInput.x, 0, movementInput.y) * moveSpeed); 
                if (Input.GetButton("Jump"))
                {
                    _moveDir.y = jumpSpeed;
                }
                }   
            _moveDir.y -= _gravity * Time.deltaTime;
            _charC.Move(_moveDir * Time.deltaTime);
        }
    }
}

