using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMovements : MonoBehaviour
{

    float horizontal;
    [SerializeField]float speed;
    [SerializeField]bool headOn = true;
    Rigidbody body;
    float move;
    Collider collider;

    bool isGrounded;
    Vector3 jump;
    float jumpForce = 2.0f;
    [SerializeField]float rayDistance;
    //A CORRIGER
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask buttonLayer;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        jump = new Vector3(0, 2f, 0);
    }

    void Update()
    {
        if (headOn)
        {
            body.mass = 0.75f;
        }
        else if (!headOn)
        {
            body.mass = 1.25f;
        }

        horizontal = Input.GetAxis("Horizontal");
        move = speed * horizontal;

        CheckGroundStatus();

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            body.AddForce(jump * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector3(move,body.velocity.y,body.velocity.z);
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
