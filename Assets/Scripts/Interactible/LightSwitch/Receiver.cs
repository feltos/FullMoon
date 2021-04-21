using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    [SerializeField] bool isActive = false;
    MeshRenderer meshRenderer;
    [SerializeField] Material activeMaterial;
    [SerializeField] Material unactiveMaterial;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        if (isActive)
        {
            meshRenderer.material = activeMaterial;
        }
        else
        {
            meshRenderer.material = unactiveMaterial;
        }
    }

    public void Activate()
    {
        isActive = true;
    }
}
