using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class HeadMovements : MonoBehaviour
{
    float horizontal;
    float vertical;
    float horizontalMovement;
    float verticalMovement;
    [SerializeField] float speed;
    Rigidbody body;
    [SerializeField] GameObject player;
    BodyMovements bodyMovements;
    bool canAttach = false;
    [SerializeField] Transform headPos;
    bool damp = true;

    //Rotation of the head
    //public float degreesPerSecond = 15.0f;
    //public float amplitude = 0.5f;
    //public float frequency = 1f;

    [SerializeField] float strength;
    bool moving;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        bodyMovements = GetComponentInParent<BodyMovements>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("HorizontalHead");
        vertical = Input.GetAxis("VerticalHead");

        if(Input.GetJoystickNames().Length > 0)
        {
            horizontalMovement = InputManager.ActiveDevice.RightStickX * speed;
            verticalMovement = InputManager.ActiveDevice.RightStickY * speed;
        }
        else
        {
            horizontalMovement = horizontal * speed;
            verticalMovement = vertical * speed;
        }

        if (damp)
        {
            transform.position = Vector3.Lerp(transform.position, headPos.position, speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, headPos.position) <= 0.05f)
            {
                transform.position = headPos.position;
            }
        }

        AttachHead();
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

    void AttachHead()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (transform.parent != null)
            {
                damp = false;
                transform.parent = null;
                bodyMovements.SetHeadOn(false);
            }
            else if (transform.parent == null && GetCanAttach() == true)
            {
                damp = true;
                transform.parent = player.transform;
                bodyMovements.SetHeadOn(true);
            }
        }
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
