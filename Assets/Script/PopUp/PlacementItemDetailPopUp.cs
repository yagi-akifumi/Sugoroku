using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlacementItemDetailPopUp : MonoBehaviour
{
    [SerializeField]
    private Text txtItemDetail;


    [SerializeField]
    private Button btnYes;

    [SerializeField]
    private Button btnNo;

    private ItemData currentItemData;

    [SerializeField, Header("キャンバス")]
    private CanvasGroup canvasGroup;

    private ItemListGenerator itemListGenerator;

    public void SetUpPlacementItemDetailPopUp(ItemListGenerator itemListGenerator)
    {
        this.itemListGenerator = itemListGenerator;

        // 各ボタンの操作を押せない状態にする
        SwitchActivateButtons(false);

        // 各ボタンにスクリプトの設定
        btnYes.onClick.AddListener(OnClickYes);
        btnNo.onClick.AddListener(HidePopUp);

        // 各ボタンを押せる状態にする
        SwitchActivateButtons(true);
    }

    /// <summary>
    /// 各ボタンのアクティブ状態の切り替え
    /// </summary>
    /// <param name="isSwitch"></param>
    public void SwitchActivateButtons(bool isSwitch)
    {
        btnYes.interactable = isSwitch;
        btnNo.interactable = isSwitch;
    }

    /// <summary>
    /// ポップアップの表示
    /// </summary>

    public void ShowPopUp()
    {
        if (currentItemData == null)
        {
            Debug.LogError("currentItemDataがありません");
            return;
        }

        if (currentItemData.itemType == ItemType.Equipment)
        {
            txtItemDetail.text = "装備しますか？";
        }
        else if (currentItemData.itemType == ItemType.Consumable)
        {
            txtItemDetail.text = "アイテムを使いますか？";
        }
        else if (currentItemData.itemType == ItemType.EventItem)
        {
            txtItemDetail.text = "このアイテムを確認しますか？";
        }

        canvasGroup.DOFade(1.0f, 0.5f);
    }

    public void SetItemData(ItemData itemData)
    {
        currentItemData = itemData;
    }

    /// <summary>
    /// ポップアップの非表示
    /// </summary>
    public void HidePopUp()
    {
        // ポップアップの非表示
        Debug.Log("閉じるボタン実装");
        canvasGroup.DOFade(0.0f, 0.5f).OnComplete(() => itemListGenerator.InActivatePlacementItemDetailPopUp());

        /// <summary>
        /// アイテムリストの更新
        /// </summary>
    }

    private void OnClickYes()
    {
        if(currentItemData==null)
        {
            return;
        }
        if(currentItemData.itemType==ItemType.Consumable)
        {
            UseCurrentItem();
        }
        else if(currentItemData.itemType == ItemType.Equipment)
        {
            EquipCurrentItem();
        }
        else if(currentItemData.itemType==ItemType.EventItem)
        {
            Debug.Log("イベントアイテムです");
        }
        itemListGenerator.UpdateItemListPopUp();
        HidePopUp();
    }

    private void UseCurrentItem()
    {
        bool result = GameData.instance.UseItem(currentItemData.itemId);

        if (result)
        {
            Debug.Log("アイテムを使用しました: " + currentItemData.itemName);

            // ここに後で効果を入れる
            // 例: GameData.instance.life += 3;
        }
        else
        {
            Debug.Log("アイテムを使用できませんでした");
        }

        
    }

    private void EquipCurrentItem()
    {
        Debug.Log("装備処理: " + currentItemData.itemName);

        // 今は仮
        // 後で GameData.instance.equippedItemId = currentItemData.itemId;
    }
}
