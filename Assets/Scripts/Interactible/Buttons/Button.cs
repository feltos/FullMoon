using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] bool isPushed = false;
    Vector3 basePosition;
    Vector3 desiredPosition;
    MeshRenderer meshRenderer;
    [SerializeField] Material pushMaterial;
    [SerializeField] Material heldMaterial;
    enum buttonType
    {
        SIMPLE_BUTTON,
        BUTTON_TO_HELD
    }
    [SerializeField] buttonType button;

    private void Start()
    {
        basePosition = transform.position;
        desiredPosition = basePosition - transform.up / 100 * 15;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        switch (button)
        {
            case buttonType.SIMPLE_BUTTON:

                meshRenderer.material = pushMaterial;

                if (isPushed)
                {
                    transform.position = Vector3.Lerp(transform.position, desiredPosition, 3 * Time.deltaTime);
                }

                break;

            case buttonType.BUTTON_TO_HELD:

                meshRenderer.material = heldMaterial;

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
            Debug.Log("Pushed");
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
