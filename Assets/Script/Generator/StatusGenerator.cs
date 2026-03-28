using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusGenerator : MonoBehaviour
{
    [SerializeField, Header("ステータスPrefab")]
    private PlacementStatusPopUp placementStatusPopUpPrefab;
    private PlacementStatusPopUp placementStatusPopUp;

    [SerializeField, Header("キャンバス")]
    private Transform canvasTran;

    public CanvasGroup mainCanvas;

    public void SetUpStatusGenerator()
    {
        CreatePlacementStatusPopUp();
    }

    private void CreatePlacementStatusPopUp()
    {
        // ポップアップを生成
        placementStatusPopUp = Instantiate(placementStatusPopUpPrefab, canvasTran, false);

        // ポップアップの設定
        placementStatusPopUp.SetUpPlacementStatusPopUp(this);

        // ポップアップを非表示にする
        placementStatusPopUp.gameObject.SetActive(false);
    }

    /// <summary>
    /// ステータスの表示
    /// </summary>
    public void ActivatePlacementStatusPopUp()
    {

        // placementStatusPopUpがnullではないことを確認
        if (placementStatusPopUp != null)
        {
            // アイテムのポップアップの表示
            placementStatusPopUp.gameObject.SetActive(true);
            placementStatusPopUp.ShowPopUp();
        }
        else
        {
            Debug.LogError("placementStatusPopUpがnullです。");
        }
    }

    /// <summary>
    /// ステータスの非表示
    /// </summary>
    public void InActivatePlacementStatusPopUp()
    {
        //Debug.Log("非表示にするよ");
        placementStatusPopUp.gameObject.SetActive(false);
    }
}
