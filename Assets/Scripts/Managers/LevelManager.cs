using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] GameObject[] checkpoints;

    //S'effectue si le joueur est touché
    public void CheckLastCheckpoint(Transform playerPos)
    {
        for(int i = 0; i < checkpoints.Length; i++)
        {
            if(checkpoints[i].GetComponent<Checkpoints>().GetStatut() == false)
            {
                playerPos.position = checkpoints[i - 1].transform.position;
                break;         
            }
            else if( i == checkpoints.Length -1 && checkpoints[i].GetComponent<Checkpoints>().GetStatut() == true)
            {
                playerPos.position = checkpoints[i].transform.position;
                break;
            }
        }
    }
}
