using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class HeadReflector : MonoBehaviour
{

    //Ray system 
    float maxLength;
    LayerMask layerToHide;
    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;
    [SerializeField] float rotationSpeed;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        layerToHide = ~(1 << LayerMask.NameToLayer("PlayerHead") | 1 << LayerMask.NameToLayer("HeadDetectionZone"));
        maxLength = 0;
    }
    void Update()
    {
        //Ray if hit by Laser
        ray = new Ray(transform.position, transform.right);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength, layerToHide))
        {
            lineRenderer.positionCount += 1;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
            remainingLength -= Vector3.Distance(ray.origin, hit.point);

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Receiver"))
            {
                hit.collider.gameObject.GetComponent<Receiver>().Activate();
            }

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                hit.collider.gameObject.GetComponent<PlayerLifeSystem>().TakingDamage();
            }
        }

        //Rotate object for direction of ray 
        if (InputManager.ActiveDevice.RightTrigger)
        {
            transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        if (InputManager.ActiveDevice.LeftTrigger)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        maxLength = 0;
    }

    public void ActivateReflector()
    {
        maxLength = 100;
    }
}
