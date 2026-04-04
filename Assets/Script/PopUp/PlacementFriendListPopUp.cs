using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlacementFriendListPopUp : MonoBehaviour
{
    [SerializeField]
    private Button btnClose;

    [SerializeField]
    private SelectFriend selectFriendPrefab;

    private FriendGenerator friendGenerator;

    [SerializeField]
    private Transform canvasTran;

    [SerializeField, Header("キャンバス")]
    private CanvasGroup canvasGroup;

    public void SetUpPlacementFriendListPopUp()
    {

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
    /// ポップアップの非表示
    /// </summary>
    public void ShowPopUp()
    {
        // ポップアップの非表示
        canvasGroup.DOFade(1.0f, 0.5f);
    }

    /// <summary>
    /// ポップアップの非表示
    /// </summary>
    public void HidePopUp()
    {
        // ポップアップの非表示
        canvasGroup.DOFade(0.0f, 0.5f).OnComplete(() => friendGenerator.InActivatePlacementFriendListPopUp());
    }
}
