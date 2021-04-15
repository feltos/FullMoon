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
        //Store bool state of buttons 
        for(int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].layer == LayerMask.NameToLayer("ButtonSimple"))
            {
                buttonsState.Add(buttons[i].GetComponentInChildren<ButtonSimple>().GetIsPushed());
            }
            else if(buttons[i].layer == LayerMask.NameToLayer("ButtonToHeld"))
            {
                buttonsState.Add(buttons[i].GetComponentInChildren<ButtonHeld>().GetIsPushed());
            }
        }
        basePosition = transform.position;
        desiredPosition = new Vector3(basePosition.x, basePosition.y + 2.5f, basePosition.z);
    }

    void Update()
    {
        
        UpdateButtonState();
        CheckIfCanOpen();

        if (isOpen)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, openingSpeed * Time.deltaTime);
        }

        if (!isOpen)
        {
            transform.position = Vector3.Lerp(transform.position, basePosition, openingSpeed * Time.deltaTime);
        }
    }

    void CheckIfCanOpen()
    {
        if (buttonsState.TrueForAll(b => b))
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }
    }

    void UpdateButtonState()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].GetComponentInChildren<ButtonHeld>() != null)
            {
                if (buttons[i].GetComponentInChildren<ButtonHeld>().GetIsPushed() == true)
                {
                    buttonsState[i] = true;
                }
                else
                {
                    buttonsState[i] = false;
                }
            }
            if (buttons[i].GetComponentInChildren<ButtonSimple>() != null)
            {
                if (buttons[i].GetComponentInChildren<ButtonSimple>().GetIsPushed() == true)
                {
                    buttonsState[i] = true;
                }
                else
                {
                    buttonsState[i] = false;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach(GameObject button in buttons)
        {
            Debug.DrawLine(transform.position, button.transform.GetChild(0).position);
        }        
    }
}
