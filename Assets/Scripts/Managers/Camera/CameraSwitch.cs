using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class CameraSwitch : MonoBehaviour
{
    float speed = 10;
    [SerializeField] Transform playerFocus;
    [SerializeField] Transform headFocus;

    Vector3 offset = new Vector3(1,1,1);
    float minPosX;
    float maxPosX;
    float minPosY;
    float maxPosY;

    [SerializeField] Transform minPosTransformX;
    [SerializeField] Transform maxPosTransformX;

    [SerializeField] Transform minPosTransformY;
    [SerializeField] Transform maxPosTransformY;

    //[SerializeField]CinemachineVirtualCamera virtualCamera;

    enum CameraState
    {
        BODY,
        HEAD
    }

    CameraState cameraState = CameraState.BODY;


    private void Awake()
    {
        minPosX = minPosTransformX.position.x - offset.x;
        maxPosX = maxPosTransformX.position.x + offset.x;

        minPosY = minPosTransformY.position.y - 4;
        maxPosY = maxPosTransformY.position.y + offset.y;


    }

    Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = Camera.main.orthographicSize;
        float camWidth = Camera.main.orthographicSize * Camera.main.aspect;

        float minX = minPosX + camWidth;
        float maxX = maxPosX - camWidth;
        float minY = minPosY + camHeight;
        float maxY = maxPosY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
    private void Update()
    {
        switch (cameraState)
        {
            case CameraState.BODY:

                transform.position = Vector3.Lerp(transform.position, playerFocus.transform.position, speed * Time.deltaTime);
                transform.position = ClampCamera(transform.position);

                if (InputManager.ActiveDevice.RightBumper.IsPressed)
                {
                    cameraState = CameraState.HEAD;
                }


                break;

            case CameraState.HEAD:

                transform.position = Vector3.Lerp(transform.position, headFocus.transform.position, speed * Time.deltaTime);
                transform.position = ClampCamera(transform.position);

                if (InputManager.ActiveDevice.LeftBumper.IsPressed)
                {
                    cameraState = CameraState.BODY;
                }

                break;
        }


    }
}
