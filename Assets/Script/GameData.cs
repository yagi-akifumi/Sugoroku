using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameData : MonoBehaviour
{
    public static GameData instance;

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

    // ===== 装備 =====
    public int equippedHeadId = -1;
    public int equippedBodyId = -1;
    public int equippedLegsId = -1;
    public int equippedAccessoryId = -1;



    [System.Serializable]
    public class ItemInventoryData
    {
        public int itemCount;   // 所持数
        public int itemId;      // アイテムID
    }

    [Header("所持アイテムのリスト")]
    public List<ItemInventoryData> itemInventoryDatasList = new List<ItemInventoryData>();

    [System.Serializable]
    public class FriendShipData
    {
        public int friendNum;      // 友達ID
        public int friendShip;     // 友情度
    }

    [Header("友情リスト")]
    public List<FriendShipData> friendShipDatasList = new List<FriendShipData>();


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

        equippedHeadId = -1;
        equippedBodyId = -1;
        equippedLegsId = -1;
        equippedAccessoryId = -1;


        itemInventoryDatasList.Clear();
        InitFriendShipData();

        Debug.Log("テスト");
    }

    // ===== 友達リスト初期化 =====
    public void InitFriendShipData()
    {
        friendShipDatasList.Clear();

        foreach (FriendData friendData in DataBaseManager.instance.friendDataSO.friendDatasList)
        {
            FriendShipData newData = new FriendShipData();
            newData.friendNum = friendData.friendNum;
            newData.friendShip = 0;

            friendShipDatasList.Add(newData);
        }
    }


    private void Awake()
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
        ItemInventoryData item = itemInventoryDatasList.Find(x => x.itemId == itemId);

        if (item != null)
        {
            item.itemCount++;
        }
        else
        {
            ItemInventoryData newItem = new ItemInventoryData();
            newItem.itemId = itemId;
            newItem.itemCount = 1;
            itemInventoryDatasList.Add(newItem);
        }

        Debug.Log("アイテム追加: " + itemId);
    }

    public bool UseItem(int itemId)
    {
        ItemInventoryData item = itemInventoryDatasList.Find(x => x.itemId == itemId);

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

        item.itemCount--;

        if (item.itemCount == 0)
        {
            itemInventoryDatasList.Remove(item);
        }

        Debug.Log("アイテム使用: " + itemId);
        return true;
    }

    public bool HasItem(int itemId)
    {
        ItemInventoryData item = itemInventoryDatasList.Find(x => x.itemId == itemId);

        if (item == null)
        {
            return false;
        }

        return item.itemCount > 0;
    }

    public int GetItemCount(int itemId)
    {
        ItemInventoryData item = itemInventoryDatasList.Find(x => x.itemId == itemId);

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

    public bool EquipItem(int itemId)
    {
        if (!HasItem(itemId))
        {
            Debug.Log("そのアイテムを持っていません");
            return false;
        }

        ItemData itemData = DataBaseManager.instance.GetItemDataById(itemId);

        if (itemData == null)
        {
            Debug.LogError("itemDataが見つかりません");
            return false;
        }

        if (itemData.itemType != ItemType.Equipment)
        {
            Debug.Log("装備アイテムではありません");
            return false;
        }

        switch (itemData.equipmentType)
        {
            case EquipmentType.Head:
                equippedHeadId = itemId;
                break;

            case EquipmentType.Body:
                equippedBodyId = itemId;
                break;

            case EquipmentType.Legs:
                equippedLegsId = itemId;
                break;

            case EquipmentType.Accessory:
                equippedAccessoryId = itemId;
                break;

            default:
                Debug.Log("装備部位が不正です");
                return false;
        }

        Debug.Log("装備成功: " + itemData.itemName);
        return true;
    }

    public bool UnEquipItem(int itemId)
    {
        if (equippedHeadId == itemId)
        {
            equippedHeadId = -1;
            return true;
        }

        if (equippedBodyId == itemId)
        {
            equippedBodyId = -1;
            return true;
        }

        if (equippedLegsId == itemId)
        {
            equippedLegsId = -1;
            return true;
        }

        if (equippedAccessoryId == itemId)
        {
            equippedAccessoryId = -1;
            return true;
        }

        Debug.Log("装備されていないアイテムです");
        return false;
    }

    public bool IsEquipped(int itemId)
    {
        return equippedHeadId == itemId
            || equippedBodyId == itemId
            || equippedLegsId == itemId
            || equippedAccessoryId == itemId;
    }

    

    public int GetTotalLife()
    {
        return life + GetEquipmentLifeBonus();
    }

    public int GetTotalPower()
    {
        return power + GetEquipmentPowerBonus();
    }

    public int GetTotalIntelligence()
    {
        return intelligence + GetEquipmentIntelligenceBonus();
    }

    public int GetTotalCoolness()
    {
        return coolness + GetEquipmentCoolnessBonus();
    }

    public int GetTotalMorality()
    {
        return morality + GetEquipmentMoralityBonus();
    }

    public int GetTotalKindness()
    {
        return kindness + GetEquipmentKindnessBonus();
    }

    private int GetEquipmentPowerBonus()
    {
        int total = 0;

        total += GetItemPower(equippedHeadId);
        total += GetItemPower(equippedBodyId);
        total += GetItemPower(equippedLegsId);
        total += GetItemPower(equippedAccessoryId);

        return total;
    }

    private int GetEquipmentLifeBonus()
    {
        int total = 0;

        total += GetItemLife(equippedHeadId);
        total += GetItemLife(equippedBodyId);
        total += GetItemLife(equippedLegsId);
        total += GetItemLife(equippedAccessoryId);

        return total;
    }

    private int GetEquipmentIntelligenceBonus()
    {
        int total = 0;

        total += GetItemIntelligence(equippedHeadId);
        total += GetItemIntelligence(equippedBodyId);
        total += GetItemIntelligence(equippedLegsId);
        total += GetItemIntelligence(equippedAccessoryId);

        return total;
    }

    private int GetEquipmentCoolnessBonus()
    {
        int total = 0;

        total += GetItemCoolness(equippedHeadId);
        total += GetItemCoolness(equippedBodyId);
        total += GetItemCoolness(equippedLegsId);
        total += GetItemCoolness(equippedAccessoryId);

        return total;
    }

    private int GetEquipmentMoralityBonus()
    {
        int total = 0;

        total += GetItemMorality(equippedHeadId);
        total += GetItemMorality(equippedBodyId);
        total += GetItemMorality(equippedLegsId);
        total += GetItemMorality(equippedAccessoryId);

        return total;
    }

    private int GetEquipmentKindnessBonus()
    {
        int total = 0;

        total += GetItemKindness(equippedHeadId);
        total += GetItemKindness(equippedBodyId);
        total += GetItemKindness(equippedLegsId);
        total += GetItemKindness(equippedAccessoryId);

        return total;
    }

    private int GetItemPower(int itemId)
    {
        if (itemId < 0) return 0;

        ItemData itemData = DataBaseManager.instance.GetItemDataById(itemId);

        if (itemData == null) return 0;

        return itemData.power;
    }

    private int GetItemLife(int itemId)
    {
        if (itemId < 0) return 0;

        ItemData itemData = DataBaseManager.instance.GetItemDataById(itemId);

        if (itemData == null) return 0;

        return itemData.life;
    }

    private int GetItemIntelligence(int itemId)
    {
        if (itemId < 0) return 0;

        ItemData itemData = DataBaseManager.instance.GetItemDataById(itemId);

        if (itemData == null) return 0;

        return itemData.intelligence;
    }

    private int GetItemCoolness(int itemId)
    {
        if (itemId < 0) return 0;

        ItemData itemData = DataBaseManager.instance.GetItemDataById(itemId);

        if (itemData == null) return 0;

        return itemData.coolness;
    }

    private int GetItemMorality(int itemId)
    {
        if (itemId < 0) return 0;

        ItemData itemData = DataBaseManager.instance.GetItemDataById(itemId);

        if (itemData == null) return 0;

        return itemData.morality;
    }

    private int GetItemKindness(int itemId)
    {
        if (itemId < 0) return 0;

        ItemData itemData = DataBaseManager.instance.GetItemDataById(itemId);

        if (itemData == null) return 0;

        return itemData.kindness;
    }

    public int GetFriendship(int friendNum)
    {
        FriendShipData data = friendShipDatasList.Find(x => x.friendNum == friendNum);

        if (data == null)
        {
            Debug.LogWarning("友情データなし: " + friendNum);
            return 0;
        }

        return data.friendShip;
    }

}