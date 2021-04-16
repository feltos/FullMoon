using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidePlatform : MonoBehaviour
{

    float distance;
    GameObject playerHead;
    bool isCloser = false;
    Vector3 basePosition;
    Vector3 desiredPosition;
    [SerializeField] float deltaPosition;
    [SerializeField] float movingSpeed;

    private void Start()
    {
        playerHead = GameObject.Find("PlayerHead");
        basePosition = transform.position;
        desiredPosition = new Vector3(basePosition.x, basePosition.y + deltaPosition, basePosition.z);
    }
    void Update()
    {

        if (isCloser)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, movingSpeed * Time.deltaTime);
        }

        if (!isCloser)
        {
            transform.position = Vector3.Lerp(transform.position, basePosition, movingSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("HeadDetectionZone"))
        {
            isCloser = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("HeadDetectionZone"))
        {
            isCloser = false;
        }
    }

 
}
