using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectItem : MonoBehaviour
{
    [SerializeField]
    private Button btnSelectItem;

    [SerializeField]
    private Text txtItemName;

    [SerializeField]
    private Text txtItemCount;

    public void SetItemData(ItemData itemData, int itemCount)
    {
        txtItemName.text = itemData.itemName;
        txtItemCount.text = "×" + itemCount;
    }
}
