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


    public void SetUpUIManager()
    {
        btnDice.onClick.AddListener(OnClickDice);
        btnStatus.onClick.AddListener(() => statusGenerator.ActivatePlacementStatusPopUp());
    }


    public void OnClickDice()
    {
        int dice = Random.Range(1, 7);
        Debug.Log("出目: " + dice);
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