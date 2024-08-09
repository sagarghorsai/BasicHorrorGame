using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public GameObject openUI, Door, closeUI;
    public Animator doorAnim;
    public bool open, interactable;
    public bool needKey;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (!open)
            {
                openUI.SetActive(true);
                interactable = true;
                closeUI.SetActive(false);
                GameManager.instance.SetCurrentInteractor(gameObject);
            }
            else
            {
                closeUI.SetActive(true);
                interactable = true;
                openUI.SetActive(false);
                GameManager.instance.SetCurrentInteractor(gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            openUI.SetActive(false);
            closeUI.SetActive(false);
            interactable = false;
            GameManager.instance.ClearCurrentInteractor(gameObject);
        }
    }

    private void Update()
    {
        if (GameManager.instance.currentInteractor == gameObject && interactable == true && !needKey)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!open)
                {
                    open = true;
                    doorAnim.ResetTrigger("DoorOpen");
                    doorAnim.SetTrigger("DoorOpen");
                }
                else
                {
                    open = false;
                    doorAnim.ResetTrigger("DoorClose");
                    doorAnim.SetTrigger("DoorClose");
                }
            }
        }
        else if (GameManager.instance.currentInteractor == gameObject && needKey)
        {
            Debug.Log("Need Key!");
        }
    }
}
