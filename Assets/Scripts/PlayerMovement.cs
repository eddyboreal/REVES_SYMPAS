using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jump_height = 3f;

    //allows to check if Player is grounded
    public Transform GroundCheck;
    public float ground_distance = 0.4f;
    public LayerMask GroundMask;

    bool is_grounded;

    Vector3 velocity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        is_grounded = Physics.CheckSphere(GroundCheck.position, ground_distance, GroundMask);
        if(is_grounded && velocity.y < 0f)
        {
            //force Player to the ground
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //target the direction where the Player wants to move (according to its transform)
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && is_grounded)
        {
            Debug.Log("Jump");
            velocity.y = Mathf.Sqrt(jump_height * -2f * gravity);
        }

        //simulate gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
