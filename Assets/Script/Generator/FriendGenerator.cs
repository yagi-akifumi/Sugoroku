using UnityEngine;
using System.Collections.Generic;

public class FriendGenerator : MonoBehaviour
{
    [SerializeField, Header("フレンドリストPrefab")]
    private PlacementFriendListPopUp placementFriendListPopUpPrefab;
    private PlacementFriendListPopUp placementFriendListPopUp;

    [SerializeField, Header("フレンドリストPrefab")]
    private PlacementFriendDetailPopUp placementFriendDetailPopUpPrefab;
    private PlacementFriendDetailPopUp placementFriendDetailPopUp;

    [SerializeField, Header("キャンバス")]
    private Transform canvasTran;

    public void SetUpFriendGenerator()
    {
        CreatePlacementFriendListPopUp();
        CreatePlacementFriendDetailPopUp();
    }

    private void CreatePlacementFriendListPopUp()
    {
        placementFriendListPopUp = Instantiate(placementFriendListPopUpPrefab, canvasTran, false);
        placementFriendListPopUp.SetUpPlacementFriendListPopUp(this);
        placementFriendListPopUp.gameObject.SetActive(false);
    }

    private void CreatePlacementFriendDetailPopUp()
    {
        placementFriendDetailPopUp = Instantiate(placementFriendDetailPopUpPrefab, canvasTran, false);
        placementFriendDetailPopUp.SetUpPlacementFriendDetailPopUp(this);
        placementFriendDetailPopUp.gameObject.SetActive(false);
    }

    public void ActivatePlacementFriendListPopUp(List<int> friendNumList)
    {
        if (placementFriendListPopUp != null)
        {
            placementFriendListPopUp.gameObject.SetActive(true);
            placementFriendListPopUp.SetFriendList(friendNumList);
            placementFriendListPopUp.ShowPopUp();
        }
        else
        {
            Debug.LogError("placementFriendListPopUpがnullです。");
        }
    }

    public void ActivatePlacementFriendDetailPopUp(int friendNum)
    {
        if (placementFriendDetailPopUp != null)
        {
            FriendData friendData = DataBaseManager.instance.GetFriendDataById(friendNum);

            if (friendData == null)
            {
                Debug.LogError("friendDataがnullです。 friendNum: " + friendNum);
                return;
            }

            placementFriendDetailPopUp.gameObject.SetActive(true);
            placementFriendDetailPopUp.SetFriendData(friendData);
            placementFriendDetailPopUp.ShowPopUp();
        }
        else
        {
            Debug.LogError("placementFriendDetailPopUpがnullです。");
        }
    }

    public void InActivatePlacementFriendListPopUp()
    {
        placementFriendListPopUp.gameObject.SetActive(false);
    }

    public void InActivatePlacementFriendDetailPopUp()
    {
        placementFriendDetailPopUp.gameObject.SetActive(false);
    }

}
