using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadMovements : MonoBehaviour
{
    float horizontal;
    float vertical;
    [SerializeField] float speed;
    Collider col;
    [SerializeField] GameObject player;
    bool canAttach = false;
    private ParticleSystem particlesSystem;
    void Start()
    {        
        col = GetComponent<Collider>();
        particlesSystem = player.GetComponent<ParticleSystem>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(transform.parent != null)
        {
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
