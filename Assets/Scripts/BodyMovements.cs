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

    bool isGrounded = true;
    Vector3 jump;
    float jumpForce = 2.0f;

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

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            body.AddForce(jump * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector3(move,body.velocity.y,body.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;        
    }

    public void SetHeadOn()
    {
        headOn = !headOn;
    }
}
