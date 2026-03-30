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

    [System.Serializable]
    public class ItemInventoryData
    {
        public int itemCount;            // 所持数
        public int itemId;           // 所持している通し番号
    }

    [Header("所持アイテムのリスト")]
    public List<ItemInventoryData> itemInventoryDatasList = new List<ItemInventoryData>();

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

    public void AddItem(int itemId)
    {
        // すでに持ってるか探す
        ItemInventoryData item = itemInventoryDatasList
            .Find(x => x.itemId == itemId);

        if (item != null)
        {
            // 持ってたら数を増やす
            item.itemCount++;
        }
        else
        {
            // 持ってなかったら新しく追加
            ItemInventoryData newItem = new ItemInventoryData();
            newItem.itemId = itemId;
            newItem.itemCount = 1;

            itemInventoryDatasList.Add(newItem);
        }

        Debug.Log("アイテム追加: " + itemId);
    }

    public bool UseItem(int itemId)
    {
        // 持ってるか探す
        ItemInventoryData item = itemInventoryDatasList
            .Find(x => x.itemId == itemId);

        if (item == null)
        {
            Debug.Log("アイテムを持っていない");
            return false;
        }

        if (item.itemCount <= 0)
        {
            Debug.Log("個数が足りない");
            return false;
        }

        // 1個減らす
        item.itemCount--;

        // 0になったらリストから削除
        if (item.itemCount == 0)
        {
            itemInventoryDatasList.Remove(item);
        }

        Debug.Log("アイテム使用: " + itemId);
        return true;
    }

    public bool HasItem(int itemId)
    {
        ItemInventoryData item = itemInventoryDatasList
            .Find(x => x.itemId == itemId);

        if (item == null)
        {
            return false;
        }

        return item.itemCount > 0;
    }

    public int GetItemCount(int itemId)
    {
        ItemInventoryData item = itemInventoryDatasList
            .Find(x => x.itemId == itemId);

        if (item == null)
        {
            return 0;
        }

        return item.itemCount;
    }

    public bool BuyItem(ItemData itemData)
    {

        if (money < itemData.itemPrice)
        {
            Debug.Log("お金が足りない");
            return false;
        }

        money -= itemData.itemPrice;
        AddItem(itemData.itemId);

        Debug.Log("購入成功: " + itemData.itemId);
        return true;
    }
}
