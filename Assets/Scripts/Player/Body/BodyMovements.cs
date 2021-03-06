using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

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
    float noHeadGravity;
    [SerializeField] float jumpForce;

    Transform latePos;

    Vector3 moveVector;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        noHeadGravity = baseGravity * 6;       
    }

    void Update()
    {
        if (headOn)
        {
            gravity = baseGravity;
        }
        else if (!headOn)
        {
            gravity = noHeadGravity;
        }

        CheckGroundStatus();
  
        if (isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) || InputManager.ActiveDevice.Action1.IsPressed)
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
        moveVector = new Vector3(move, verticalVelocity, 0);
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
