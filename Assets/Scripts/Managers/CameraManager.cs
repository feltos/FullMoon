using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class CameraManager : MonoBehaviour
{
    Transform actualPosition;
    [SerializeField]Transform unzoomPosition;
    float speed = 10;
    [SerializeField] Transform player;

    enum CameraState
    {
        ZOOM,
        UNZOOM
    }

    CameraState cameraState = CameraState.ZOOM;

    private void Update()
    {
        switch (cameraState)
        {
            case CameraState.UNZOOM:

                transform.position = Vector3.Lerp(transform.position, unzoomPosition.position, speed * Time.deltaTime);

                if (InputManager.ActiveDevice.RightTrigger.WasReleased)
                {
                    cameraState = CameraState.ZOOM;
                }
                break;

            case CameraState.ZOOM:

                transform.position = Vector3.Lerp(transform.position, player.position, speed * Time.deltaTime);

                if (InputManager.ActiveDevice.RightTrigger.WasPressed)
                {
                    cameraState = CameraState.UNZOOM;
                }
                break;
        }
    }
}
