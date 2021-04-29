using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadMovements : MonoBehaviour
{
    float horizontal;
    float vertical;
    float horizontalMovement;
    float verticalMovement;
    [SerializeField] float speed;
    Rigidbody body;
    Collider col;
    [SerializeField] GameObject player;
    bool canAttach = false;
    private ParticleSystem particlesSystem;
    [SerializeField]Transform headPos;

    //Rotation of the head
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    void Start()
    {        
        col = GetComponent<Collider>();
        body = GetComponent<Rigidbody>();
        particlesSystem = player.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("HorizontalHead");
        vertical = Input.GetAxis("VerticalHead");
        horizontalMovement = horizontal * speed;
        verticalMovement = vertical * speed;

        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
    }

    private void FixedUpdate()
    {
        if(transform.parent == null)
        {
            body.velocity = new Vector3(horizontalMovement, verticalMovement, 0);
        }

        if (transform.parent != null)
        {
            transform.position = headPos.position;
            particlesSystem.Pause();
            particlesSystem.Clear();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && transform.parent == null)
        {
            canAttach = true;
            particlesSystem.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (transform.parent == null)
        {
            canAttach = false;
            particlesSystem.Pause();
            particlesSystem.Clear();
        }
    }

    public bool GetCanAttach()
    {
        return canAttach;
    }
}
