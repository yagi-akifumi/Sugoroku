using UnityEngine;

public class FriendGenerator : MonoBehaviour
{
    [SerializeField, Header("フレンドリストPrefab")]
    private PlacementFriendListPopUp placementFriendListPopUpPrefab;
    private PlacementFriendListPopUp placementFriendListPopUp;

    [SerializeField, Header("キャンバス")]
    private Transform canvasTran;

    public void SetUpFriendGenerator()
    {
        CreatePlacementFriendListPopUp();
        //CreatePlacementFriendDetailPopUp();
    }

    private void CreatePlacementFriendListPopUp()
    {
        placementFriendListPopUp = Instantiate(placementFriendListPopUpPrefab, canvasTran, false);
        placementFriendListPopUp.SetUpPlacementFriendListPopUp();
        placementFriendListPopUp.gameObject.SetActive(false);
    }

    public void ActivatePlacementFriendListPopUp()
    {
        if (placementFriendListPopUp != null)
        {
            placementFriendListPopUp.gameObject.SetActive(true);
            placementFriendListPopUp.ShowPopUp();
        }
        else
        {
            Debug.LogError("placementItemListPopUpがnullです。");
        }
    }

    public void InActivatePlacementFriendListPopUp()
    {
        placementFriendListPopUp.gameObject.SetActive(false);
    }



}
