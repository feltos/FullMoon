using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    float basicSpeed;
    [SerializeField] ParticleSystem explosion;

    private void Start()
    {
        basicSpeed = movementSpeed;
        DestroyObject(this.gameObject, 7f);
    }

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * movementSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("TRIGGER");
            Vector3 direction = collision.gameObject.transform.position - transform.position;
            collision.gameObject.GetComponent<PlayerLifeSystem>().TakingDamage(direction, 5);
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("HeadDetectionZone"))
        {
            movementSpeed = movementSpeed / 2;
        }     
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("HeadDetectionZone"))
        {
            movementSpeed = basicSpeed;
        }
    }

 

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
