using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment,
    EventItem,
}

public enum EquipmentType
{
    None,
    Head,
    Body,
    Legs,
    Accessory
}

[System.Serializable]
public class ItemData
{
    public int itemId;
    public string itemName;
    public int itemPrice;
    public ItemType itemType;

    [TextArea(2, 5)]
    public string description;

    [Header("装備用（Equipmentのみ有効）")]
    public EquipmentType equipmentType = EquipmentType.None;

    public int power;
    public int life;
    public int intelligence;
    public int coolness;
    public int morality;
    public int kindness;
    public int money;

    public Sprite avatarSprite;

    public bool IsEquipment()
    {
        return itemType == ItemType.Equipment;
    }
}