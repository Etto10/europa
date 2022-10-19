using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
    public ItemType itemType;
    public string description;

    public enum ItemType
    {
        Placeable,
        Item,
        NeedsPlate,
        Food,
        Seed,
        Analyzer
    }
}
