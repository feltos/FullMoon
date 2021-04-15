using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeSystem : MonoBehaviour
{
    [SerializeField] int playerlife;
    Rigidbody body;
    [SerializeField] float explosionforce;
    bool canBeHit = true;
    float hitTimer = 3f;
    Scene actualScene;
    Scene lastScene;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        actualScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (!canBeHit)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0)
            {
                canBeHit = true;
                hitTimer = 3f;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Harmful") && canBeHit)
        {
            playerlife -= 1;
            if (playerlife <= 0)
            {
                SceneManager.LoadScene(actualScene.name);
            }
            canBeHit = false;

        }
    }
}
