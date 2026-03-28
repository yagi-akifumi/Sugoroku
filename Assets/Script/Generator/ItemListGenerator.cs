using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListGenerator : MonoBehaviour
{
    [SerializeField, Header("アイテムリストPrefab")]
    private PlacementItemListPopUp placementItemListPopUpPrefab;
    private PlacementItemListPopUp placementItemListPopUp;

    [SerializeField, Header("キャンバス")]
    private Transform canvasTran;

    public CanvasGroup mainCanvas;

    public void SetUpItemListGenerator()
    {
        CreatePlacementItemListPopUp();
    }


    private void CreatePlacementItemListPopUp()
    {
        // ポップアップを生成
        placementItemListPopUp = Instantiate(placementItemListPopUpPrefab, canvasTran, false);

        // ポップアップの設定
        placementItemListPopUp.SetUpPlacementItemListPopUp(this);

        // ポップアップを非表示にする
        placementItemListPopUp.gameObject.SetActive(false);
    }

    /// <summary>
    /// ステータスの表示
    /// </summary>
    public void ActivatePlacementItemListPopUp()
    {

        // placementStatusPopUpがnullではないことを確認
        if (placementItemListPopUp != null)
        {
            // アイテムのポップアップの表示
            placementItemListPopUp.gameObject.SetActive(true);
            placementItemListPopUp.ShowPopUp();
        }
        else
        {
            Debug.LogError("placementItemListPopUpがnullです。");
        }
    }

    /// <summary>
    /// アイテムリストの非表示
    /// </summary>
    public void InActivatePlacementItemListPopUp()
    {
        Debug.Log("非表示にするよ");
        placementItemListPopUp.gameObject.SetActive(false);
    }
}
