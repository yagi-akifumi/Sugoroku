using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum DiceState
{
    Idle,
    Rolling,
}

public class DiceManager : MonoBehaviour
{
    [Header("サイコロデータ")]
    public DiceDataSO diceDataSO;

    [Header("表示先")]
    public Image diceImage;

    [SerializeField]
    private CanvasGroup diceCanvasGroup;

    private DiceData currentDiceData;

    public DiceState currentState = DiceState.Idle;

    public void RollDice(Action<int> onComplete)
    {
        //サイコロが回転中
        if (currentState == DiceState.Rolling)
        {
            Debug.Log("サイコロは回転中です");
            return;
        }

        //DiceDataSOが設定されていない
        if (diceDataSO == null)
        {
            Debug.LogError("DiceDataSOが設定されていません");
            currentState = DiceState.Idle;
            return;
        }

        //diceDatasList にデータが入っていない
        if (diceDataSO.diceDatasList == null || diceDataSO.diceDatasList.Count == 0)
        {
            Debug.LogError("diceDatasList にデータが入っていません");
            currentState = DiceState.Idle;
            return;
        }

        currentState = DiceState.Rolling;
        SetDiceVisible(true);
        StartCoroutine(RollDiceCoroutine(onComplete));
    }

    private IEnumerator RollDiceCoroutine(Action<int> onComplete)
    {
        float totalTime = 2.5f;   // 演出全体の長さ
        float elapsed = 0f;
        float interval = 0.05f;   // 最初の切り替え速度

        while (elapsed < totalTime)
        {
            int randomIndex = UnityEngine.Random.Range(0, diceDataSO.diceDatasList.Count);
            DiceData shuffleData = diceDataSO.diceDatasList[randomIndex];

            if (diceImage != null)
            {
                diceImage.sprite = shuffleData.diceSprite;
            }

            yield return new WaitForSeconds(interval);

            elapsed += interval;
            interval += 0.01f;
        }

        int finalIndex = UnityEngine.Random.Range(0, diceDataSO.diceDatasList.Count);
        currentDiceData = diceDataSO.diceDatasList[finalIndex];

        if (diceImage != null)
        {
            diceImage.sprite = currentDiceData.diceSprite;
        }

        yield return new WaitForSeconds(1.0f);

        currentState = DiceState.Idle;

        onComplete?.Invoke(currentDiceData.diceNum);
    }

    public int GetCurrentDiceNum()
    {
        if (currentDiceData == null)
        {
            return 0;
        }

        return currentDiceData.diceNum;
    }

    public void SetDiceVisible(bool isVisible)
    {
        if (diceCanvasGroup != null)
        {
            diceCanvasGroup.alpha = isVisible ? 1f : 0f;
            diceCanvasGroup.interactable = false;
            diceCanvasGroup.blocksRaycasts = false;
        }
        else if (diceImage != null)
        {
            // CanvasGroup未設定時の保険
            Color color = diceImage.color;
            color.a = isVisible ? 1f : 0f;
            diceImage.color = color;
        }
    }

    public void InitializeDiceCanvasGroup()
    {
        if (diceCanvasGroup != null)
        {
            diceCanvasGroup.alpha = 0f;
            diceCanvasGroup.interactable = false;
            diceCanvasGroup.blocksRaycasts = false;
        }

        currentState = DiceState.Idle;
    }
}
