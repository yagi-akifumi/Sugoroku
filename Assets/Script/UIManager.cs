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
    private Text txtDiceTurn;

    [SerializeField]
    private StatusGenerator statusGenerator;

    [SerializeField]
    private ItemListGenerator itemListGenerator;

    [SerializeField]
    private DiceManager diceManager;

    public void SetUpUIManager()
    {
        btnDice.onClick.AddListener(OnClickDice);
        btnStatus.onClick.AddListener(() => statusGenerator.ActivatePlacementStatusPopUp());
        btnItemList.onClick.AddListener(() => itemListGenerator.ActivatePlacementItemListPopUp());

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