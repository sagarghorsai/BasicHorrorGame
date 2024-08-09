using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PickupFlashlight : MonoBehaviour
{
    public Item flashlightItem;  // Reference to the flashlight item
    public GameObject flashlight_table; // The flashlight on the table
    public GameObject pickupUI;       // UI or 3D object indicating interaction
    public bool interactable;           // Whether the flashlight is interactable
    private Inventory playerInventory;  // Reference to the player's inventory
    private PhotonView view;            // Reference to the PhotonView

    private void Start()
    {
        // Find the player object and get the Inventory component
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        view = GetComponent<PhotonView>(); // Get the PhotonView component
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
        // Ensure only the local player can interact with the flashlight
        if (view.IsMine && GameManager.instance.currentInteractor == gameObject && interactable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickupUI.SetActive(false);
                interactable = false;

                // Check if the player has space in their inventory
                if (playerInventory.AddItem(flashlightItem))
                {
                    // Send an RPC to all players to remove the flashlight from the table
                    view.RPC("PickupFlashlightRPC", RpcTarget.All);
                    Destroy(flashlight_table);
                }
                else
                {
                    Debug.Log("Inventory is full.");
                }
            }
        }
    }

  
}
