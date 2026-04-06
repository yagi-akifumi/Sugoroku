using UnityEngine;
using UnityEngine.UI;

public class SelectFriend : MonoBehaviour
{
    [SerializeField]
    private Button btnSelect;

    [SerializeField]
    private Text txtFriendName;

    [SerializeField]
    private Text txtFriendShip;

    [SerializeField]
    private Image imgFriendPicture;

    private FriendData currentFriendData;
    private FriendGenerator friendGenerator;

    public void SetFriendData(FriendData friendData, FriendGenerator friendGenerator)
    {
        currentFriendData = friendData;
        this.friendGenerator = friendGenerator;

        if (currentFriendData == null)
        {
            Debug.LogError("currentFriendDataがありません");
            return;
        }

        txtFriendName.text = currentFriendData.friendName;
        int friendship = GameData.instance.GetFriendship(currentFriendData.friendNum);
        txtFriendShip.text = friendship.ToString();
        imgFriendPicture.sprite = currentFriendData.friendPicture;
        imgFriendPicture.enabled = (currentFriendData.friendPicture != null);

        btnSelect.onClick.RemoveAllListeners();
        btnSelect.onClick.AddListener(OnClickFriend);
    }

    private void OnClickFriend()
    {
        if (currentFriendData == null)
        {
            Debug.LogError("currentFriendDataがありません");
            return;
        }

        if (friendGenerator == null)
        {
            Debug.LogError("friendGeneratorがありません");
            return;
        }

        friendGenerator.ActivatePlacementFriendDetailPopUp(currentFriendData.friendNum);
    }
}