using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovements : MonoBehaviour
{
    float horizontal;
    float vertical;
    [SerializeField] float speed;
    Collider col;
    [SerializeField] GameObject player;
    bool canAttach = false;

    void Start()
    {        
        col = GetComponent<Collider>();
    }

    void Update()
    {    

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canAttach = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canAttach = false;
    }

    public bool GetCanAttach()
    {
        return canAttach;
    }
}
