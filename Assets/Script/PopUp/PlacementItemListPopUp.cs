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

    // ===== アバター =====
    [SerializeField]
    private Image imgHead;

    [SerializeField]
    private Image imgBody;

    [SerializeField]
    private Image imgLegs;

    [SerializeField]
    private Image imgAccessory;

    // ===== デフォルト画像 =====
    [Header("デフォルト画像")]
    [SerializeField] private Sprite defaultHead;
    [SerializeField] private Sprite defaultBody;
    [SerializeField] private Sprite defaultLegs;

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
        UpdateItemList();
        UpdateAvatarView();
        canvasGroup.DOFade(1.0f, 0.5f);
    }

    /// <summary>
    /// ポップアップの非表示
    /// </summary>
    public void HidePopUp()
    {
        // ポップアップの非表示
        canvasGroup.DOFade(0.0f, 0.5f).OnComplete(() => itemListGenerator.InActivatePlacementItemListPopUp());
    }

    /// <summary>
    /// アイテムリストの更新
    /// </summary>

    public void UpdateItemList()
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
                bool isEquipped = GameData.instance.IsEquipped(itemData.itemId);
                newSelectItem.SetItemData(itemData, inventoryData.itemCount, itemListGenerator, isEquipped);
                selectItemsList.Add(newSelectItem);
            }
        }

        UpdateAvatarView();

        Debug.Log("アイテムアップデート実装完了");
    }

    public void UpdateAvatarView()
    {
        SetAvatarPart(imgHead, GameData.instance.equippedHeadId, defaultHead, true);
        SetAvatarPart(imgBody, GameData.instance.equippedBodyId, defaultBody, true);
        SetAvatarPart(imgLegs, GameData.instance.equippedLegsId, defaultLegs, true);

        // アクセは未装備なら非表示
        SetAvatarPart(imgAccessory, GameData.instance.equippedAccessoryId, null, false);
    }

    private void SetAvatarPart(Image targetImage, int itemId, Sprite defaultSprite, bool useDefault)
    {
        if (targetImage == null)
        {
            return;
        }

        if (itemId < 0)
        {
            if (useDefault)
            {
                targetImage.sprite = defaultSprite;
                targetImage.enabled = (defaultSprite != null);
            }
            else
            {
                targetImage.sprite = null;
                targetImage.enabled = false;
            }
            return;
        }

        ItemData itemData = DataBaseManager.instance.GetItemDataById(itemId);

        if (itemData == null || itemData.avatarSprite == null)
        {
            if (useDefault)
            {
                targetImage.sprite = defaultSprite;
                targetImage.enabled = (defaultSprite != null);
            }
            else
            {
                targetImage.sprite = null;
                targetImage.enabled = false;
            }
            return;
        }

        targetImage.sprite = itemData.avatarSprite;
        targetImage.enabled = true;
    }

}
