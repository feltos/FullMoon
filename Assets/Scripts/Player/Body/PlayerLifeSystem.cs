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
    LevelManager levelManager;


    void Start()
    {
        actualScene = SceneManager.GetActiveScene();
        cc = GetComponent<CharacterController>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

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

    public void TakingDamage()
    {
        if (canBeHit)
        {
            levelManager.CheckLastCheckpoint(this.transform);
            canBeHit = false;
        }
    }

}
