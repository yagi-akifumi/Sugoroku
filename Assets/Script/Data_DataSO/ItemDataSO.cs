using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "Create ItemDataSO")]
public class ItemDataSO : ScriptableObject
{
    public int itemId;
    public string itemName;
    public int itemPrice;
    public ItemType itemType;
    public string description;
}