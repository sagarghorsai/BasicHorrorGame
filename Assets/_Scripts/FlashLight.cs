using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject light;
    public bool toggle;
    public AudioSource toggleSound;

    private void Start()
    {
        if (toggle == false)
        {
            light.SetActive(false);
        }

    if(toggle == true)
        {
            light.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            toggle = !toggle;
            if(toggle==false)
            {
                light.SetActive(false);
            }

            if (toggle == true)
            {
                light.SetActive(true);
            }
        }
    }



}
