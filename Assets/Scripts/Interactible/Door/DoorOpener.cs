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
    [SerializeField] BoxCollider collider;

    private void Start()
    {
        //Store bool state of buttons 
        for(int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].layer == LayerMask.NameToLayer("Button"))
            {
                buttonsState.Add(buttons[i].GetComponentInChildren<Button>().GetIsPushed());
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

    void UpdateButtonState()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].GetComponentInChildren<Button>().GetIsPushed() == true)
            {
                buttonsState[i] = true;
            }
            else
            {
                buttonsState[i] = false;
            }
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

    private void OnDrawGizmos()
    {
        foreach(GameObject button in buttons)
        {
            Debug.DrawLine(transform.position, button.transform.GetChild(0).position);
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<PlayerLifeSystem>().TakingDamage(Vector3.zero, 5);
        }
    }
}
