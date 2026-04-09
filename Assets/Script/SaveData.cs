using UnityEngine;
using System.Collections.Generic;
using System;// システムを省略

// Serializable →class版のシリアライズフィールド
[Serializable]
public class SaveData : object
{
    // 保存しておくべきデータ
    public bool isSaveData;

    public string playerName;
    public int currentTurn;
    public int playerIndex;

    public int life;
    public int power;
    public int intelligence;
    public int coolness;
    public int morality;
    public int kindness;
    public int money;


    [Serializable]
    public class SaveItemInventoryData
    {
        public int itemId;
        public int itemCount;
    }

    public List<SaveItemInventoryData> itemInventoryDatasList = new List<SaveItemInventoryData>();

}