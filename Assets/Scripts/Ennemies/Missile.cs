using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * movementSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
