using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    [SerializeField]List<GameObject> buttons;
    [SerializeField] List<bool> buttonsState;
    int buttonNumber = 0;
    bool isOpen = false;
    Vector3 basePosition;
    Vector3 desiredPosition;
    [SerializeField] float openingSpeed;

    private void Start()
    {
        foreach (GameObject Button in buttons)
        {
            if (Button.GetComponent<ButtonSimple>().GetIsPushed() == true)
            {
                
            }
        }
        buttonNumber = buttons.Count;
        basePosition = transform.position;
        desiredPosition = new Vector3(basePosition.x, basePosition.y + 2.5f, basePosition.z);
    }

    void Update()
    {
        //foreach (GameObject Button in buttons)
        //{
        //    if (Button.GetComponent<ButtonSimple>().GetIsPushed() == true)
        //    {

        //    }
        //}

        if (Input.GetKeyDown(KeyCode.T))
        {
            isOpen = !isOpen;
        }

        if (isOpen)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, openingSpeed * Time.deltaTime);
        }

        if (!isOpen)
        {
            transform.position = Vector3.Lerp(transform.position, basePosition, openingSpeed * Time.deltaTime);
        }
    }
}
