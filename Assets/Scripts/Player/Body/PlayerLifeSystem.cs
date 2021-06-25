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
    Vector3 impact = Vector3.zero;
    float mass = 3;
    LevelManager levelManager;

    BodyMovements bodyMovements;

    enum DamageType
    {
        KNOCKBACK,
        CHECKPOINT
    }
    [SerializeField] DamageType damage;

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
                hitTimer = 0.2f;
            }
        }

        // apply the impact force:
        if (impact.magnitude > 0.2)
        {
            cc.Move(impact * Time.deltaTime);
        }
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    public void TakingDamage(Vector3 direction, float force)
    {
        if (canBeHit)
        {
            if(damage == DamageType.CHECKPOINT)
            {
                levelManager.CheckLastCheckpoint(this.transform);
            }

            if(damage == DamageType.KNOCKBACK)
            {
                addImpact(direction, force);
                canBeHit = false;
            }
        }
    }

    void addImpact(Vector3 direction, float force)
    {
        direction.z = 0;
        direction.Normalize();
        if(direction.y < 0)
        {
            direction.y = -direction.y;
        }
        impact += new Vector3( direction.x * force * 5, (direction.y * force) / 2, 0);
    }

}
