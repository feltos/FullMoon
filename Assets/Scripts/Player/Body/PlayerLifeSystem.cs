using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class PlayerLifeSystem : MonoBehaviour
{
    [SerializeField] int playerlife;
    Rigidbody body;
    [SerializeField] float explosionforce;
    [SerializeField] bool canBeHit = true;
    float hitTimer = 3f;
    Scene actualScene;
    Scene lastScene;

    CharacterController cc;
    [SerializeField] ParticleSystem explosion;
    Vector3 lastPos;
    float lastPosTimer = 1;

    void Start()
    {
        actualScene = SceneManager.GetActiveScene();
        cc = GetComponent<CharacterController>();
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

        lastPosTimer -= Time.deltaTime;
        if(lastPosTimer <= 0)
        {
            lastPos = transform.position;
            lastPosTimer = 1;
        }
    }


    void RespawnBody(Transform transform)
    {
        cc.enabled = false;
        transform.position = lastPos;
        cc.enabled = true;
    }


    public void TakingDamage()
    {
        if (canBeHit)
        {
            //playerlife -= 1;
            //if (playerlife <= 0)
            //{
            //    SceneManager.LoadScene(actualScene.name);
            //}
            RespawnBody(transform);
            canBeHit = false;
        }
        RespawnBody(transform);
    }

}
