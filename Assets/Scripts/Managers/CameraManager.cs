using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] Transform lookAt;
    [SerializeField] float boundX;
    [SerializeField] float boundY;

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        //X axis
        float dx = lookAt.position.x - transform.position.x;
        if(dx > boundX || dx < -boundX)
        {
            if(transform.position.x < lookAt.position.x)
            {
                delta.x = dx - boundX;
            }
            else
            {
                delta.x = dx + boundX;
            }
        }

        //Y axis
        float dy = lookAt.position.y - transform.position.y;
        if (dy > boundX || dy < -boundX)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = dy - boundY;
            }
            else
            {
                delta.y = dy + boundY;
            }
        }

        //Move camera
        transform.position = transform.position + delta;
    }
}
