using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] ParticleSystem explosion;

    private void Start()
    {
        DestroyObject(this.gameObject, 7f);
    }

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * movementSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
