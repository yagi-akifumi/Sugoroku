using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    [Header("サイコロデータ")]
    public DiceDataSO diceDataSO;

    [Header("表示先")]
    public Image diceImage;

    private DiceData currentDiceData;

    public int RollDice()
    {
        if (diceDataSO == null)
        {
            Debug.LogError("DiceDataSOが設定されていません");
            return -1;
        }

        if (diceDataSO.diceDatasList == null || diceDataSO.diceDatasList.Count == 0)
        {
            Debug.LogError("diceDatasList にデータが入っていません");
            return -1;
        }

        int randomIndex = Random.Range(0, diceDataSO.diceDatasList.Count);
        currentDiceData = diceDataSO.diceDatasList[randomIndex];

        if (diceImage != null)
        {
            diceImage.sprite = currentDiceData.diceSprite;
        }

        return currentDiceData.diceNum;
    }

    public int GetCurrentDiceNum()
    {
        if (currentDiceData == null)
        {
            return 0;
        }

        return currentDiceData.diceNum;
    }
}