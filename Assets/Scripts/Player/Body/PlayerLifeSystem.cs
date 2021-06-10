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
    float hitTimer = 1f;
    Scene actualScene;
    Scene lastScene;

    CharacterController cc;
    LevelManager levelManager;

    BodyMovements bodyMovements;
     

    void Start()
    {
        actualScene = SceneManager.GetActiveScene();
        cc = GetComponent<CharacterController>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        bodyMovements = gameObject.GetComponent<BodyMovements>();
    }

    void Update()
    {
        if (!canBeHit)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0)
            {
                canBeHit = true;
                hitTimer = 1f;
            }
        }
    }

    public void TakingDamage(Vector3 direction)
    {
        if (canBeHit)
        {
            //levelManager.CheckLastCheckpoint(this.transform);
            canBeHit = false;
        }
    }

}
