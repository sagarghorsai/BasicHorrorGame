using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
    public Image icon;
    public Text itemName;

    public void SetItem(Item item)
    {
        itemName.text = item.itemName;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        itemName.text = "";
        icon.sprite = null;
        icon.enabled = false;
    }
}
