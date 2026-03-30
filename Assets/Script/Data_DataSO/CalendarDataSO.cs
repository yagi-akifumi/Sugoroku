using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CalendarDataSO", menuName = "Create CalendarDataSO")]
public class CalendarDataSO : ScriptableObject
{
    public List<CalendarData> calendarDatasList = new List<CalendarData>();
}