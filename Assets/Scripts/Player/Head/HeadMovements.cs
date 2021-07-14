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

    [SerializeField] float strength;
    bool moving;

    int frameTP;

    enum State
    {
        ONBODY,
        FREE,
        EJECTED,
        ATTIRED,
        ONREPOP
    }

    State headState = State.ONBODY;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        bodyMovements = GetComponentInParent<BodyMovements>();
    }

    void Update()
    {
        switch (headState)
        {
            case State.ONBODY:

                if (Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    transform.parent = null;
                    bodyMovements.SetHeadOn(false);
                    headState = State.EJECTED;
                }

                break;

            case State.FREE:

                horizontal = Input.GetAxis("HorizontalHead");
                vertical = Input.GetAxis("VerticalHead");

                if (Input.GetJoystickNames().Length > 0)
                {
                    horizontalMovement = InputManager.ActiveDevice.RightStickX * speed;
                    verticalMovement = InputManager.ActiveDevice.RightStickY * speed;
                }
                else
                {
                    horizontalMovement = horizontal * speed;
                    verticalMovement = vertical * speed;
                }

                body.velocity = new Vector3(horizontalMovement, verticalMovement, 0);

                if (Vector3.Distance(transform.position, headPos.transform.position) < 1.5)
                {
                    headState = State.ATTIRED;
                }

                if (Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    body.velocity = Vector3.zero;
                    headState = State.ONREPOP;
                }

                break;

            case State.EJECTED:

                float randDirX = Random.Range(-2, 2);

                body.AddForce(new Vector3(randDirX, 1, 0), ForceMode.Impulse);
                if (Vector3.Distance(transform.position, headPos.transform.position) > 2)
                {
                    body.velocity = Vector3.zero;
                    headState = State.FREE;
                }

                break;

            case State.ATTIRED:

                transform.position = Vector3.Lerp(transform.position, headPos.position, speed);
                if (Vector3.Distance(transform.position, headPos.position) <= 0.05f)
                {
                    transform.position = headPos.position;
                    transform.parent = player.transform;
                    bodyMovements.SetHeadOn(true);
                    headState = State.ONBODY;
                }

                break;

            case State.ONREPOP:


                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    frameTP += 1;
                }

                if(frameTP >= 120)
                {
                    transform.position = headPos.position;
                    transform.parent = player.transform;
                    bodyMovements.SetHeadOn(true);
                    frameTP = 0;
                    headState = State.ONBODY;
                }

                if (Input.GetKeyUp(KeyCode.LeftAlt))
                {
                    frameTP = 0;
                    headState = State.FREE;
                }

                break;
        }
        Debug.DrawLine(transform.position, headPos.transform.position);
        Debug.Log(headState);
    }
}
