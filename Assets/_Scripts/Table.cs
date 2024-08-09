using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public GameObject openUI,closeUI, bottom;
    public Animator openAnim;
    public bool open, interactable;
    public AudioSource OpenSound, CloseSound;

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
        if (GameManager.instance.currentInteractor == gameObject && interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!open)
                {
                    open = true;
                    openAnim.ResetTrigger("TableOpen");
                    openAnim.SetTrigger("TableOpen");
                }
                else
                {
                    open = false;
                    openAnim.ResetTrigger("TableClose");
                    openAnim.SetTrigger("TableClose");
                }
            }
        }
    }
}
