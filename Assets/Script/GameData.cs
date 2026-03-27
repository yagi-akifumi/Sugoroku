using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameData : MonoBehaviour
{
    public static GameData instance;        // シングルトンデザインパターンのクラスにするための変数

    [Header("セーブデータの有無")]
    public bool isSaveData;

    private string saveKey = "UserData";

    // ===== ターン =====
    public int currentTurn = 0;
    public int maxTurn = 12;

    // ===== プレイヤー位置 =====
    public int playerIndex = 0;

    // ===== ステータス =====
    public int life = 0;
    public int power = 0;
    public int intelligence = 0;
    public int coolness = 0;
    public int morality = 0;
    public int kindness = 0;
    public int money = 0;


    // ===== 初期化 =====
    public void Init()
    {
        currentTurn = 0;
        playerIndex = 0;

        life = 0;
        power = 0;
        intelligence = 0;
        coolness = 0;
        morality = 0;
        kindness = 0;
        money = 0;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
