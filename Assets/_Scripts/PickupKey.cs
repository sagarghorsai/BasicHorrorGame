using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKey : MonoBehaviour
{
    public Item keyItem;  // Reference to the flashlight item
    public GameObject keyObject; // The flashlight on the table
    public GameObject pickupUI;       // UI or 3D object indicating interaction
    public bool interactable;           // Whether the flashlight is interactable
    private Inventory playerInventory;  // Reference to the player's inventory

    private void Start()
    {
        // Find the player object and get the Inventory component
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the player is looking at the flashlight
        if (other.CompareTag("MainCamera"))
        {
            interactable = true;
            pickupUI.SetActive(true);
            GameManager.instance.SetCurrentInteractor(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Hide the interaction indicator when the player looks away
        if (other.CompareTag("MainCamera"))
        {
            pickupUI.SetActive(false);
            interactable = false;
        }
    }

    private void Update()
    {
        if (GameManager.instance.currentInteractor == gameObject && interactable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickupUI.SetActive(false);
                interactable = false;

                if (playerInventory.AddItem(keyItem))
                {
                    Destroy(keyObject);
                }
                else
                {
                    Debug.Log("Inventory is full.");
                }
            }
        }
    }
}
