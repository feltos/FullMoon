using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    float speed = 10;
    [SerializeField] Transform playerFocus;
    [SerializeField] Transform headFocus;

    Vector3 offset;
    [SerializeField] MeshRenderer MapSize;
    float minPosX;
    float maxPosX;
    float minPosY;
    float maxPosY;

    //[SerializeField]CinemachineVirtualCamera virtualCamera;

    enum CameraState
    {
        BODY,
        HEAD
    }

    CameraState cameraState = CameraState.BODY;


    private void Awake()
    {
        minPosX = MapSize.transform.position.x - MapSize.bounds.size.x / 2f;
        maxPosX = MapSize.transform.position.x + MapSize.bounds.size.x / 2f;

        minPosY = MapSize.transform.position.y - MapSize.bounds.size.y / 2f;
        maxPosY = MapSize.transform.position.y + MapSize.bounds.size.y / 2f;


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
