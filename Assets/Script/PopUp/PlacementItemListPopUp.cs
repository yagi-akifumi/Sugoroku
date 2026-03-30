using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlacementItemListPopUp : MonoBehaviour
{
    [SerializeField]
    private Button btnClose;

    [SerializeField]
    private SelectItem selectItemPrefab;

    [SerializeField]
    private List<SelectItem> selectItemsList = new List<SelectItem>();

    private ItemData chooseItemData;

    private ItemListGenerator itemListGenerator;

    [SerializeField]
    private Transform itemListParent;

    [SerializeField, Header("キャンバス")]
    private CanvasGroup canvasGroup;

    public void SetUpPlacementItemListPopUp(ItemListGenerator itemListGenerator)
    {
        this.itemListGenerator = itemListGenerator;

        // 各ボタンの操作を押せない状態にする
        SwitchActivateButtons(false);

        // 各ボタンにスクリプトの設定
        Debug.Log("閉じるボタン設定");
        btnClose.onClick.AddListener(HidePopUp);

        // 各ボタンを押せる状態にする
        SwitchActivateButtons(true);
    }


    /// <summary>
    /// 各ボタンのアクティブ状態の切り替え
    /// </summary>
    /// <param name="isSwitch"></param>
    public void SwitchActivateButtons(bool isSwitch)
    {
        btnClose.interactable = isSwitch;
    }


    /// <summary>
    /// ポップアップの表示
    /// </summary>
    public void ShowPopUp()
    {
        // ポップアップの表示
        UpdateItemList();
        canvasGroup.DOFade(1.0f, 0.5f);
    }

    /// <summary>
    /// ポップアップの非表示
    /// </summary>
    public void HidePopUp()
    {
        // ポップアップの非表示
        Debug.Log("閉じるボタン実装");
        canvasGroup.DOFade(0.0f, 0.5f).OnComplete(() => itemListGenerator.InActivatePlacementItemListPopUp());
    }

    /// <summary>
    /// アイテムリストの更新
    /// </summary>

    private void UpdateItemList()
    {
        foreach (SelectItem item in selectItemsList)
        {
            Destroy(item.gameObject);
        }
        selectItemsList.Clear();

        foreach (GameData.ItemInventoryData inventoryData in GameData.instance.itemInventoryDatasList)
        {
            SelectItem newSelectItem = Instantiate(selectItemPrefab, itemListParent);

            ItemData itemData = DataBaseManager.instance.GetItemDataById(inventoryData.itemId);

            if (itemData != null)
            {
                newSelectItem.SetItemData(itemData, inventoryData.itemCount, itemListGenerator);
                selectItemsList.Add(newSelectItem);
            }
        }
        Debug.Log("アイテムアップデート実装完了");
    }

}
