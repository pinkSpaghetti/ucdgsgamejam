using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float gravity = -2f;


    private Vector3 move;
    private float moveX;
    private float moveY;
    private bool isPressing = false;
    private float maxSpeed;
    private CharacterController cc;

    public float jumpHeight;
    bool jump;

    public Transform groundCheck;
    public float groundDist;
    public LayerMask groundMask;
    bool isGrounded;

    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /***
        if (isPressing)
        {
            if (moveX > 0)
            { Move(moveX); }

            if (moveX < 0)
            { Move(moveX); }

            if (moveY > 0)
            { Move(moveY); }

            if (moveY < 0)
            { Move(moveY); }
        }s
        

        // Reset isPressing
        isPressing = !isPressing;
        ***/
        // set player x and z input to vector
        move = new Vector3(moveX, 0, moveY);

        // move player based on input and move speed
        cc.Move(new Vector3(moveX, 0, 0));

        if (jump)
        {
            // check if player is grounded
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

            if (isGrounded && jump)
            {
                Debug.Log("jump true");
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jump = false;
            }

            else if (!isGrounded && jump)
            {
                Debug.Log("jump false");
                jump = false;
            }
        }


        // gravity
        velocity.y += gravity * Time.fixedDeltaTime;
        cc.Move(velocity * Time.fixedDeltaTime);
       

    }

    public void Move(InputAction.CallbackContext context) => moveX = context.ReadValue<float>();

    public void Jump(InputAction.CallbackContext context) { if (context.performed) jump = true; }

}