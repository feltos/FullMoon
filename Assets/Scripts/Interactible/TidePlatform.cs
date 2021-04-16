using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidePlatform : MonoBehaviour
{

    float distance;
    GameObject playerHead;

    private void Start()
    {
        playerHead = GameObject.Find("PlayerHead");
    }
    void Update()
    {
        distance = Vector3.Distance(transform.position, playerHead.transform.position);
        Debug.Log(distance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("PlayerFoot"))
        {
            other.transform.parent.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerFoot"))
        {
            other.transform.parent.gameObject.transform.SetParent(null);
        }
    }
}
