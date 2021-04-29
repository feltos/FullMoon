using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class BodyMovements : MonoBehaviour
{

    float horizontal;
    [SerializeField] float speed;
    [SerializeField] bool headOn = true;
    float move;

    bool isGrounded;
    [SerializeField] float rayDistance;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask buttonLayer;

    CharacterController characterController;
    float verticalVelocity;
    float gravity;
    [SerializeField] float baseGravity;
    float freeGravity;
    [SerializeField] float jumpForce;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        freeGravity = baseGravity * 2;
    }

    void Update()
    {
        if (headOn)
        {
            gravity = baseGravity;
        }
        else if (!headOn)
        {
            gravity = freeGravity;
        }

        CheckGroundStatus();

        if (isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (InputManager.ActiveDevice.Action1.WasPressed)
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        horizontal = Input.GetAxis("Horizontal");
        move = speed * horizontal;

        Vector3 moveVector = new Vector3(move, verticalVelocity, 0);       
        characterController.Move(moveVector * Time.deltaTime);
    }

    public void SetHeadOn(bool headOnNew)
    {
        headOn = headOnNew;
    }

    void CheckGroundStatus()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, rayDistance, groundLayer) ||
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, rayDistance, buttonLayer))
        {
            isGrounded = true;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * rayDistance, Color.green);
        }
        else
        {
            isGrounded = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * rayDistance, Color.red);
        }
    }
}
