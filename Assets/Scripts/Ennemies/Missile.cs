using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] ParticleSystem explosion;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * movementSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
