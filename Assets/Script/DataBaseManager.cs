using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    public DiceDataSO diceDataSO;
    public ItemDataSO itemDataSO;

    [SerializeField]
    private List<ItemDataSO> itemDataSOList = new List<ItemDataSO>();

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

    public ItemDataSO GetItemDataById(int itemId)
    {
        return itemDataSOList.Find(x => x.itemId == itemId);
    }
}
