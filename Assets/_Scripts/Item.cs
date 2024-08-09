using UnityEngine;

public enum ItemType
{
    Key,
    Flashlight,
    Other
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject prefab;
    public ItemType itemType;

    // For keys
    public string keyId;

    // For flashlights
    public float batteryLife;
}