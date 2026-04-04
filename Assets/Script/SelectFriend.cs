using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectFriend : MonoBehaviour
{
    [SerializeField]
    private Button btnSelectFriendDetail;

    [SerializeField]
    private Image imgFriendFace;

    [SerializeField]
    private Text txtFriendName;

    [SerializeField]
    private Text txtFriendLevel;

    private void OnClickFriendDetail()
    {
        Debug.Log("友達ボタンを押した");
    }
}