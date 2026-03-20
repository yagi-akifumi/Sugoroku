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
    private Text txtStatusGuts;

    [SerializeField]
    private Text txtStatusCoolness;

    [SerializeField]
    private Text txtStatusMorallity;

    [SerializeField]
    private Text txtStatusKindness;

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
}
