using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class CameraManager : MonoBehaviour
{
    Transform actualPosition;
    [SerializeField]    Transform unzoomPosition;
    float speed = 10;
    [SerializeField] Transform player;

    Vector3 offset;
    [SerializeField]MeshRenderer MapSize;
    float minPosX;
    float maxPosX;
    float minPosY;
    float maxPosY;

    enum CameraState
    {
        ZOOM,
        UNZOOM
    }

    CameraState cameraState = CameraState.ZOOM;


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
            case CameraState.UNZOOM:

                transform.position = Vector3.Lerp(transform.position, unzoomPosition.position, speed * Time.deltaTime);

                if (InputManager.ActiveDevice.RightTrigger.WasReleased)
                {
                    cameraState = CameraState.ZOOM;
                }
                break;

            case CameraState.ZOOM:

                transform.position = ClampCamera(transform.position);

                transform.position = Vector3.Lerp(transform.position, player.position, speed * Time.deltaTime);


                if (InputManager.ActiveDevice.RightTrigger.WasPressed)
                {
                    cameraState = CameraState.UNZOOM;
                }
                break;
        }
    }
}
