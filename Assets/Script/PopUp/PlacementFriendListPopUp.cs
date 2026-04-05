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
    private List<SelectFriend> selectFriendList = new List<SelectFriend>();

    private List<int> currentFriendNumList = new List<int>();

    [SerializeField]
    private Transform canvasTran;

    [SerializeField, Header("キャンバス")]
    private CanvasGroup canvasGroup;

    public void SetUpPlacementFriendListPopUp(FriendGenerator friendGenerator)
    {
        this.friendGenerator = friendGenerator;

        // 各ボタンの操作を押せない状態にする
        SwitchActivateButtons(false);

        // 各ボタンにスクリプトの設定
        btnClose.onClick.AddListener(HidePopUp);

        // 各ボタンを押せる状態にする
        SwitchActivateButtons(true);
    }

    public void SetFriendList(List<int> friendNumList)
    {
        currentFriendNumList = friendNumList;
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
        UpdateFriendList();
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

    public void UpdateFriendList()
    {
        foreach (SelectFriend item in selectFriendList)
        {
            Destroy(item.gameObject);
        }
        selectFriendList.Clear();

        foreach (int friendNum in currentFriendNumList)
        {
            FriendData friendData = DataBaseManager.instance.GetFriendDataById(friendNum);

            if (friendData == null)
            {
                Debug.LogWarning("friendDataが見つかりません: " + friendNum);
                continue;
            }

            SelectFriend newSelectFriend = Instantiate(selectFriendPrefab, canvasTran);
            newSelectFriend.SetFriendData(friendData, friendGenerator);

            selectFriendList.Add(newSelectFriend);
        }
    }
}
