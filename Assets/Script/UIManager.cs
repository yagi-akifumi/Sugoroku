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
    private Text txtDiceTurn;

    [SerializeField]
    private StatusGenerator statusGenerator;

    [SerializeField]
    private DiceManager diceManager;

    public void SetUpUIManager()
    {
        btnDice.onClick.AddListener(OnClickDice);
        btnStatus.onClick.AddListener(() => statusGenerator.ActivatePlacementStatusPopUp());
    }


    public void OnClickDice()
    {
        int dice = diceManager.RollDice();
        Debug.Log("出目: " + dice);
        if (dice <= 0) return;
        gameManager.MoveSteps(dice);
    }

    public void OnClickStatus()
    {
        
    }


    public void OnCountDiceTurn()
    {
        txtDiceTurn.text = gameManager.currentTurn.ToString();
    }
}