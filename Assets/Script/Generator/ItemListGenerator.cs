using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListGenerator : MonoBehaviour
{
    [SerializeField, Header("アイテムリストPrefab")]
    private PlacementItemListPopUp placementItemListPopUpPrefab;
    private PlacementItemListPopUp placementItemListPopUp;

    [SerializeField, Header("アイテムデタイルPrefab")]
    private PlacementItemDetailPopUp placementItemDetailPopUpPrefab;
    private PlacementItemDetailPopUp placementItemDetailPopUp;

    [SerializeField, Header("キャンバス")]
    private Transform canvasTran;

    public void SetUpItemListGenerator()
    {
        CreatePlacementItemListPopUp();
        CreatePlacementItemDetailPopUp();
    }

    private void CreatePlacementItemListPopUp()
    {
        placementItemListPopUp = Instantiate(placementItemListPopUpPrefab, canvasTran, false);
        placementItemListPopUp.SetUpPlacementItemListPopUp(this);
        placementItemListPopUp.gameObject.SetActive(false);
    }

    private void CreatePlacementItemDetailPopUp()
    {
        placementItemDetailPopUp = Instantiate(placementItemDetailPopUpPrefab, canvasTran, false);
        placementItemDetailPopUp.SetUpPlacementItemDetailPopUp(this);
        placementItemDetailPopUp.gameObject.SetActive(false);
    }

    public void ActivatePlacementItemListPopUp()
    {
        if (placementItemListPopUp != null)
        {
            placementItemListPopUp.gameObject.SetActive(true);
            placementItemListPopUp.ShowPopUp();
        }
        else
        {
            Debug.LogError("placementItemListPopUpがnullです。");
        }
    }

    public void ActivatePlacementItemDetailPopUp(ItemData itemData)
    {
        if (placementItemDetailPopUp != null)
        {
            placementItemDetailPopUp.gameObject.SetActive(true);
            placementItemDetailPopUp.SetItemData(itemData);
            placementItemDetailPopUp.ShowPopUp();
        }
        else
        {
            Debug.LogError("placementItemDetailPopUpがnullです。");
        }
    }

    public void InActivatePlacementItemListPopUp()
    {
        placementItemListPopUp.gameObject.SetActive(false);
    }

    public void InActivatePlacementItemDetailPopUp()
    {
        placementItemDetailPopUp.gameObject.SetActive(false);
    }
}