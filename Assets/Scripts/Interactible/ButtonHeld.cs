using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHeld : MonoBehaviour
{
    [SerializeField] bool isPushed = false;
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
            Debug.Log("POUSSER");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerFoot") || other.gameObject.layer == LayerMask.NameToLayer("PlayerHead"))
        {
            isPushed = false;
            Debug.Log("PAS POUSSER");
        }
    }

    void Update()
    {
        if (isPushed)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, 3 * Time.deltaTime);
        }

        else if (!isPushed)
        {
            transform.position = Vector3.Lerp(transform.position, basePosition, 3 * Time.deltaTime);
        }
    }
    public bool GetIsPushed()
    {
        return isPushed;
    }

}
