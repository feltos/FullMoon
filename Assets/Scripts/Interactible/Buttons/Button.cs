using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] bool isPushed = false;
    Vector3 basePosition;
    Vector3 desiredPosition;
    enum buttonType
    {
        SIMPLE_BUTTON,
        BUTTON_TO_HELD
    }
    [SerializeField] buttonType button;

    private void Start()
    {
        basePosition = transform.position;
        desiredPosition = new Vector3(basePosition.x, basePosition.y - 0.15f, basePosition.z);
    }

    void Update()
    {
        switch (button)
        {
            case buttonType.SIMPLE_BUTTON:

                if (isPushed)
                {
                    transform.position = Vector3.Lerp(transform.position, desiredPosition, 3 * Time.deltaTime);
                }

                break;

            case buttonType.BUTTON_TO_HELD:

                if (isPushed)
                {
                    transform.position = Vector3.Lerp(transform.position, desiredPosition, 3 * Time.deltaTime);
                }

                else if (!isPushed)
                {
                    transform.position = Vector3.Lerp(transform.position, basePosition, 3 * Time.deltaTime);
                }

                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerFoot") || other.gameObject.layer == LayerMask.NameToLayer("PlayerHead"))
        {
            isPushed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(button == buttonType.BUTTON_TO_HELD)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("PlayerFoot") || other.gameObject.layer == LayerMask.NameToLayer("PlayerHead"))
            {
                isPushed = false;
            }
        }
    }
    public bool GetIsPushed()
    {
        return isPushed;
    }
}
