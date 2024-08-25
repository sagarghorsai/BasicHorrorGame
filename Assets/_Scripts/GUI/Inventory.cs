using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int inventorySize = 9;
    public List<Item> inventoryItems;
    private Item selectedItem;

    public List<GameObject> inventorySlots; // Predefined inventory slots in the UI
    public GameObject playerFlashlight, playerRedKey; // Reference to the player's flashlight

    private void Start()
    {
        inventoryItems = new List<Item>(inventorySize);
        UpdateInventoryUI(); // Initialize UI
    }

    public bool AddItem(Item item)
    {
        if (inventoryItems.Count < inventorySize)
        {
            inventoryItems.Add(item);
            UpdateInventoryUI();
            return true;
        }
        return false;
    }

    public void RemoveItem(Item item)
    {
        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
            UpdateInventoryUI();
        }
    }

    public void SelectItem(int index)
    {
        if (index >= 0 && index < inventoryItems.Count)
        {
            if (selectedItem != null)
            {
                // Deactivate previous item (if necessary)
                if (selectedItem.name == "FlashLight")
                {
                    playerFlashlight.SetActive(false);
                }
                if (selectedItem.name == "RedKey")
                {
                    playerRedKey.SetActive(false);
                }
            }

            selectedItem = inventoryItems[index];
            // Activate selected item (if necessary)
            if (selectedItem.name == "FlashLight")
            {
                playerFlashlight.SetActive(true);
            }
            if (selectedItem.name == "RedKey")
            {
                playerRedKey.SetActive(true);
            }
        }
    }

    public void DeselectCurrentItem()
    {
        if (selectedItem != null && selectedItem.name == "FlashLight")
        {
            playerFlashlight.SetActive(false);
        }
        selectedItem = null;
    }

    private void UpdateInventoryUI()
    {
        Debug.Log("UPdateInventory");
        // Clear existing UI slots
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            InventorySlots slot = inventorySlots[i].GetComponent<InventorySlots>();
            if (slot != null)
            {
                if (i < inventoryItems.Count)
                {
                    slot.SetItem(inventoryItems[i]);
                }
                else
                {
                    slot.ClearSlot();
                }
            }
        }
    }
}
