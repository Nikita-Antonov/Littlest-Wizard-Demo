using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityMultiplyer;
    public bool inDialouge;

    void Start()
    {
        inDialouge = false;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!inDialouge)        //Simple check to see if the player is in a doaluge, if so, then he will stop movign
        {
            float yDir = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxis("Vertical") * moveSpeed) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed);      //Gets axis inputs for movemnt
            moveDirection = moveDirection.normalized * moveSpeed;   //Normalizes the input
            moveDirection.y = yDir;

            //Jump Mechanic by seeing if the controller is grounded
            if (controller.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }

            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityMultiplyer) * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
}
