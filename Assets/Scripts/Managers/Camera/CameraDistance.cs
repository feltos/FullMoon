using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDistance : MonoBehaviour
{
    [SerializeField] MeshRenderer MapSize;
    float minPosX;
    float maxPosX;
    float minPosY;
    float maxPosY;
    [SerializeField] Transform playerFocus;
    [SerializeField] GameObject player;
    [SerializeField] GameObject head;
    float speed = 10;



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
        float distance = Vector3.Distance(player.transform.position, head.transform.position);
        

        transform.position = Vector3.Lerp(transform.position, playerFocus.transform.position, speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -9 - distance);
        transform.position = ClampCamera(transform.position);

    }
}
