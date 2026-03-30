using System.Collections.Generic;
using UnityEngine;

public enum SeasonType
{
    Spring,
    Summer,
    Autumn,
    Winter
}

[System.Serializable]
public class CalendarData
{
    public int calendarNum;            //日付の数字
    public string calendarTxt;
    public SeasonType seasonType;
}

