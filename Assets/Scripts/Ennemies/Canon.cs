using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{

    [SerializeField] GameObject missile;
    [SerializeField] float spawnTimer;
    float startSpawnTimer;
    void Start()
    {
        startSpawnTimer = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if(spawnTimer <= 0)
        {
            SpawnMissile();
        }
    }

    void SpawnMissile()
    {
        Instantiate(missile,transform.GetChild(0).position, transform.rotation);
        spawnTimer = startSpawnTimer;
    }
}
