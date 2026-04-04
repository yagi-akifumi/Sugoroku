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

    private ItemGenerator itemGenerator;

    public void SetItemData(ItemData itemData, int itemCount, ItemGenerator itemGenerator, bool isEquipped)
    {
        currentItemData = itemData;
        this.itemGenerator = itemGenerator;

        txtItemName.text = itemData.itemName;
        txtItemCount.text = "×" + itemCount;

        if (isEquipped)
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

        itemGenerator.ActivatePlacementItemDetailPopUp(currentItemData);
    }
}

