using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlacementFriendDetailPopUp : MonoBehaviour
{
    [SerializeField]
    private Text txtFriendName;
    [SerializeField]
    private Text txtFriendIntroduction;
    [SerializeField]
    private Image imgFriendPicture;

    [SerializeField]
    private Button btnClose;

    private FriendGenerator friendGenerator;

    [SerializeField]
    private Transform canvasTran;

    [SerializeField, Header("キャンバス")]
    private CanvasGroup canvasGroup;

    private FriendData currentFriendData;

    public void SetUpPlacementFriendDetailPopUp(FriendGenerator friendGenerator)
    {
        this.friendGenerator = friendGenerator;

        // 各ボタンの操作を押せない状態にする
        SwitchActivateButtons(false);

        // 各ボタンにスクリプトの設定
        btnClose.onClick.AddListener(HidePopUp);

        // 各ボタンを押せる状態にする
        SwitchActivateButtons(true);
    }

    public void SetFriendData(FriendData friendData)
    {
        currentFriendData = friendData;
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
    /// ポップアップの非表示
    /// </summary>
    public void ShowPopUp()
    {
        if (currentFriendData == null)
        {
            Debug.LogError("currentFriendDataがありません");
            return;
        }

        txtFriendName.text = currentFriendData.friendName;
        txtFriendIntroduction.text = currentFriendData.friendIntroduction;
        imgFriendPicture.sprite = currentFriendData.friendPicture;
        imgFriendPicture.enabled = (currentFriendData.friendPicture != null);

        canvasGroup.DOFade(1.0f, 0.5f);
    }

    /// <summary>
    /// ポップアップの非表示
    /// </summary>
    public void HidePopUp()
    {
        // ポップアップの非表示
        canvasGroup.DOFade(0.0f, 0.5f).OnComplete(() => friendGenerator.InActivatePlacementFriendDetailPopUp());
    }
}
