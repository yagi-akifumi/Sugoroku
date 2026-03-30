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

    public ItemData GetItemDataById(int itemId)
    {
        return itemDataSO.itemDatasList.Find(x => x.itemId == itemId);
    }
}
