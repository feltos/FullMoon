using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight : MonoBehaviour
{

    public float visionRange;
    public float visionConeAngle;
    public bool alerted = false;
    GameObject player;
    Vector3 playerPosition;
    Vector3 vectorToPlayer;
    Light light;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        light = GetComponent<Light>();
    }

    void Update()
    {
        playerPosition = player.transform.position;
        vectorToPlayer = playerPosition - transform.position;

        Debug.Log(Vector3.Angle(transform.forward, vectorToPlayer));

        if(Vector3.Distance(transform.position, playerPosition) <= visionRange)
        {
            light.color = Color.green;

            if(Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle)
            {
                alerted = true;
            }
            else
            {
                alerted = false;
            }
        }

        if (alerted)
        {
            light.color = Color.red;
        }
    }
}
