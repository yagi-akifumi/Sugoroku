using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,   // 消耗品
    Equipment,    // 装備品
    EventItem,    // イベント用
}

[System.Serializable]
public class ItemData
{
    public int itemId;
    public string itemName;
    public int itemPrice;
    public ItemType itemType;
    public string description;           
}
