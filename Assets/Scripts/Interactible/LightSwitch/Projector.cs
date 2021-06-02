using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Projector : MonoBehaviour
{
    [SerializeField] int reflections;
    [SerializeField] float maxLength;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;
    LayerMask layerToHide;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        layerToHide = ~(1 << LayerMask.NameToLayer("PlayerHead") | 1 << LayerMask.NameToLayer("HeadDetectionZone"));
    }

    void Update()
    {
        ray = new Ray(transform.position, transform.up);
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

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("HeadReflector"))
            {
                hit.collider.gameObject.GetComponent<HeadReflector>().ActivateReflector();
            }

        }

            
    }
}
