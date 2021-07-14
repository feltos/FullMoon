using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadMovements : MonoBehaviour
{
    float horizontal;
    float vertical;
    float horizontalMovement;
    float verticalMovement;
    [SerializeField] float speed;
    Rigidbody body;
    [SerializeField] GameObject player;
    bool canAttach = false;
    [SerializeField] Transform headPos;

    //Rotation of the head
    //public float degreesPerSecond = 15.0f;
    //public float amplitude = 0.5f;
    //public float frequency = 1f;

    [SerializeField] float strength;
    bool moving;

    void Start()
    {
        body = GetComponent<Rigidbody>();      
    }

    void Update()
    {    
        horizontalMovement = horizontal * speed;
        verticalMovement = vertical * speed;
        horizontal = Input.GetAxis("HorizontalHead");
        vertical = Input.GetAxis("VerticalHead");
        Debug.Log(horizontalMovement);

        Debug.DrawLine(transform.position, headPos.transform.position);
    }

    private void FixedUpdate()
    {
        if(transform.parent == null)
        {
            body.velocity = new Vector3(horizontalMovement, verticalMovement, 0);
        }

        if (Vector3.Distance(transform.position, headPos.transform.position) <= 3)
        {
            canAttach = true;
        }
        else
        {
            canAttach = false;
        }
    }

    public bool GetCanAttach()
    {
        return canAttach;
    }

    void ResistanceCheck()
    {
        Vector3 direction = headPos.transform.position - transform.position;
        body.AddForce(strength * direction);

        if (horizontal > 0.1 || horizontal < -0.1 || vertical > 0.1 || vertical < -0.1)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (Vector3.Distance(transform.position, headPos.position) < 1 && moving)
        {
            speed = 1;
            strength = 50;
            transform.parent = null;
        }

        if (Vector3.Distance(transform.position, headPos.position) < 1 && !moving)
        {
            strength = 500;
        }

        if (Vector3.Distance(transform.position, headPos.position) > 1 && moving)
        {
            speed = 4;
            strength = 0;
        }

        if (Vector3.Distance(transform.position, headPos.position) < 0.1f && !moving)
        {
            transform.parent = player.transform;
        }
    }
}
