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
    private Text txtDiceTurn;


    public void Start()
    {
        btnDice.onClick.AddListener(OnClickDice);
    }


    public void OnClickDice()
    {
        int dice = Random.Range(1, 7);
        Debug.Log("出目: " + dice);
        gameManager.MoveSteps(dice);
    }

    public void OnCountDiceTurn()
    {
        txtDiceTurn.text = gameManager.currentTurn.ToString();
    }
}