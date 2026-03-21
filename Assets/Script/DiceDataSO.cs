using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiceDataSO", menuName = "Create DiceDataSO")]
public class DiceDataSO : ScriptableObject
{
    public List<DiceData> diceDatasList = new List<DiceData>();
}