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
    [SerializeField] LayerMask layerToHide;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        layerToHide = ~layerToHide;
    }

    void Update()
    {
        ray = new Ray(transform.position, transform.up);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength, layerToHide))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("PlayerHead"))
                {
                    lineRenderer.positionCount += 1;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);

                    if(Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength, layerToHide))
                    {
                        lineRenderer.positionCount += 1;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                        remainingLength -= Vector3.Distance(ray.origin, hit.point);
                        ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                    }
                }
                
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Receiver"))
                {
                    hit.collider.gameObject.GetComponent<Receiver>().Activate();
                }

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    hit.collider.gameObject.GetComponent<PlayerLifeSystem>().TakingDamage();
                }

            }
        }
    }
}
