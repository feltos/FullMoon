using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{

    [SerializeField] GameObject playerBody;
    [SerializeField] GameObject playerHead;
    [SerializeField] BodyMovements bodyMovements;
    [SerializeField] HeadMovements HeadMovements;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("RemoveAttachHead")) //Manette X, clavier X
        {
            if (playerHead.transform.parent != null)
            {
                playerHead.transform.parent = null;
                bodyMovements.SetHeadOn(false);
            }
            else if(playerHead.transform.parent == null && HeadMovements.GetCanAttach() == true)
            {
                playerHead.transform.parent = playerBody.transform;
                playerHead.transform.position = playerBody.transform.position + new Vector3(0, 1.25f, 0);
                bodyMovements.SetHeadOn(true);
            }
        }
    }
}
