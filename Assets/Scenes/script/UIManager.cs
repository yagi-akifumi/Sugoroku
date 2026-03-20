using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField]
    private Button btnDice;

    public void Start()
    {
        btnDice.onClick.AddListener(OnClickDice);
    }


    public void OnClickDice()
    {
        Debug.Log("ボタンがおされた！");
        gameManager.MoveOneStep();
    }
}