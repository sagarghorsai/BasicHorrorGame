using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject interactor, light;
    public bool toggle = true, interactable;
    public Renderer lightBulb;
    public Material offlight, onlight;
    public AudioSource lightSwitchSound;
    public Animator switchAnim;


     void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interactor.SetActive(true);
            interactable = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interactor.SetActive(false);
            interactable = false;

        }
    }

    private void Update()
    {
        if (interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                toggle = !toggle;
                switchAnim.ResetTrigger("Press");
                switchAnim.SetTrigger("Press");

            }
        }

        if (toggle == true)
        {
            light.SetActive(true);
            lightBulb.material = onlight;

        }

        if (toggle == false)
        {
            light.SetActive(false);
            lightBulb.material = offlight;

        }



    }




}
