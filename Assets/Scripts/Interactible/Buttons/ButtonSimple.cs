using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSimple : MonoBehaviour
{
    bool isPushed;
    Vector3 basePosition;
    Vector3 desiredPosition;

    private void Start()
    {
        basePosition = transform.position;
        desiredPosition = new Vector3(basePosition.x, basePosition.y - 0.15f, basePosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerFoot") || other.gameObject.layer == LayerMask.NameToLayer("PlayerHead"))
        {
            isPushed = true;
        }
    }

    void Update()
    {
        if (isPushed)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, 3 * Time.deltaTime);
        }
    }

    public bool GetIsPushed()
    {
        return isPushed;
    }
}
