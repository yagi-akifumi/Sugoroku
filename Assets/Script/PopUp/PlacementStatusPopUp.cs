using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlacementStatusPopUp : MonoBehaviour
{
    [SerializeField]
    private Text txtName;

    [SerializeField]
    private Text txtStatusLife;

    [SerializeField]
    private Text txtStatusPower;

    [SerializeField]
    private Text txtStatusIntelligence;

    [SerializeField]
    private Text txtStatusCoolness;

    [SerializeField]
    private Text txtStatusMorallity;

    [SerializeField]
    private Text txtStatusKindness;

    [SerializeField]
    private Text txtStatusMoney;

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


    [SerializeField]
    private Button btnClose;

    private StatusGenerator statusGenerator;

    [SerializeField, Header("キャンバス")]
    private CanvasGroup canvasGroup;

    public void SetUpPlacementStatusPopUp(StatusGenerator statusGenerator)
    {
        this.statusGenerator = statusGenerator;

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
        UpdateStatusView();
        UpdateAvatarView();
        // ポップアップの表示
        canvasGroup.DOFade(1.0f, 0.5f);
    }

    /// <summary>
    /// ポップアップの非表示
    /// </summary>
    public void HidePopUp()
    {
        // ポップアップの非表示
       
        canvasGroup.DOFade(0.0f, 0.5f).OnComplete(() => statusGenerator.InActivatePlacementStatusPopUp());
    }

    public void UpdateStatusView()
    {
        txtName.text = "Player";

        txtStatusLife.text = GameData.instance.GetTotalLife().ToString();
        txtStatusPower.text = GameData.instance.GetTotalPower().ToString();
        txtStatusIntelligence.text = GameData.instance.GetTotalIntelligence().ToString();
        txtStatusCoolness.text = GameData.instance.GetTotalCoolness().ToString();
        txtStatusMorallity.text = GameData.instance.GetTotalMorality().ToString();
        txtStatusKindness.text = GameData.instance.GetTotalKindness().ToString();
        txtStatusMoney.text = GameData.instance.money.ToString();
    }

    public void UpdateAvatarView()
    {
        SetAvatarPart(imgHead, GameData.instance.equippedHeadId, defaultHead, true);
        SetAvatarPart(imgBody, GameData.instance.equippedBodyId, defaultBody, true);
        SetAvatarPart(imgLegs, GameData.instance.equippedLegsId, defaultLegs, true);

        // ★アクセだけ例外
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
                targetImage.enabled = true;
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
                targetImage.enabled = true;
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
