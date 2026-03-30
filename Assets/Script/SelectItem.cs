using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour
{
    [SerializeField]
    private Button btnSelectItem;

    [SerializeField]
    private Text txtItemEquipment;

    [SerializeField]
    private Text txtItemName;

    [SerializeField]
    private Text txtItemCount;

    private ItemData currentItemData;

    private ItemListGenerator itemListGenerator;

    public void SetItemData(ItemData itemData, int itemCount, ItemListGenerator itemListGenerator)
    {
        currentItemData = itemData;
        this.itemListGenerator = itemListGenerator;

        txtItemName.text = itemData.itemName;
        txtItemCount.text = "×" + itemCount;

        // ★ここ追加
        if (itemData.itemType == ItemType.Equipment)
        {
            txtItemEquipment.text = "E";
        }
        else
        {
            txtItemEquipment.text = "";
        }

        btnSelectItem.onClick.RemoveAllListeners();
        btnSelectItem.onClick.AddListener(OnClickItem);
    }

    private void OnClickItem()
    {
        if (currentItemData == null)
        {
            Debug.LogError("currentItemDataがありません");
            return;
        }

        itemListGenerator.ActivatePlacementItemDetailPopUp(currentItemData);
    }
}

