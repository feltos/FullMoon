using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class CameraManager : MonoBehaviour
{
    [SerializeField]Transform actualPosition;
    [SerializeField]Transform unzoomPosition;
    public bool pressed = false;
    float speed = 10;

    private void Start()
    {
        
    }
    private void Update()
    {

        if (InputManager.ActiveDevice.RightTrigger.WasPressed)
        {
            pressed = true;
        }
        if (InputManager.ActiveDevice.RightTrigger.WasReleased)
        {
            pressed = false;
        }

        if (pressed)
        {
            Debug.Log("UNZOOM");
            transform.position = Vector3.Lerp(transform.position, unzoomPosition.position, speed * Time.deltaTime);
        }
        if (!pressed)
        {
            transform.position = Vector3.Lerp(transform.position, actualPosition.position, speed * Time.deltaTime);
        }
    }
}
