using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField]
    private Button btnDice;

    [SerializeField]
    private Button btnStatus;

    [SerializeField]
    private Button btnItemList;

    [SerializeField]
    private Button btnFriendList;

    [SerializeField]
    private Text txtDiceTurn;

    [SerializeField]
    private StatusGenerator statusGenerator;

    [SerializeField]
    private ItemGenerator itemGenerator;

    [SerializeField]
    private FriendGenerator friendGenerator;

    [SerializeField]
    private DiceManager diceManager;

    public void SetUpUIManager()
    {
        btnDice.onClick.AddListener(OnClickDice);
        btnStatus.onClick.AddListener(() => statusGenerator.ActivatePlacementStatusPopUp());
        btnItemList.onClick.AddListener(() => itemGenerator.ActivatePlacementItemListPopUp());
        btnFriendList.onClick.AddListener(OnClickFriendList);
    }


    public void OnClickDice()
    {
        btnDice.interactable = false;
        diceManager.RollDice(OnDiceRolled);
    }

    private void OnDiceRolled(int dice)
    {
        Debug.Log("出目: " + dice);

        if (dice <= 0)
        {
            btnDice.interactable = true;
            return;
        }

        gameManager.MoveSteps(dice);
    }

    public void OnCountDiceTurn()
    {
        string calendarText = DataBaseManager.instance.GetCalendarTextById(gameManager.currentTurn);

        if (string.IsNullOrEmpty(calendarText))
        {
            txtDiceTurn.text = "日付未設定";
            return;
        }

        txtDiceTurn.text = calendarText;
    }

    private void OnClickFriendList()
    {
        List<int> friendNumList = new List<int>();

        foreach (FriendData friendData in DataBaseManager.instance.friendDataSO.friendDatasList)
        {
            friendNumList.Add(friendData.friendNum);
            Debug.Log("追加したfriendNum: " + friendData.friendNum + " / " + friendData.friendName);
        }

        Debug.Log("friendNumList合計: " + friendNumList.Count);

        friendGenerator.ActivatePlacementFriendListPopUp(friendNumList);
    }

    public void SetDiceButtonInteractable(bool isActive)
    {
        btnDice.interactable = isActive;
    }

    public void SetDiceVisible(bool isVisible)
    {
        diceManager.SetDiceVisible(isVisible);
    }

    public void InitializeDice()
    {
        diceManager.InitializeDiceCanvasGroup();
    }
}