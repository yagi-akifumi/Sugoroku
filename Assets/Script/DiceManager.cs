using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    [SerializeField]
    private RectTransform diceRectTransform;

    private DiceData currentDiceData;

    public DiceState currentState = DiceState.Idle;

    private Tween bounceTween;
    private float defaultAnchoredPosY;

    private void Awake()
    {
        if (diceRectTransform != null)
        {
            defaultAnchoredPosY = diceRectTransform.anchoredPosition.y;
        }
    }

    public void RollDice(Action<int> onComplete)
    {
        if (currentState == DiceState.Rolling)
        {
            Debug.Log("サイコロは回転中です");
            return;
        }

        if (diceDataSO == null)
        {
            Debug.LogError("DiceDataSOが設定されていません");
            currentState = DiceState.Idle;
            return;
        }

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

    private void PlayDiceResultAnimation()
    {
        if (diceRectTransform == null) return;

        diceRectTransform.DOKill();
        diceRectTransform.localScale = Vector3.one;
        diceRectTransform.localRotation = Quaternion.identity;

        Sequence seq = DOTween.Sequence();

        seq.Append(diceRectTransform.DOScale(1.35f, 0.12f).SetEase(Ease.OutQuad));
        seq.Append(diceRectTransform.DOScale(1.0f, 0.18f).SetEase(Ease.OutBack));

        seq.Join(
            diceRectTransform.DOShakeAnchorPos(
                duration: 0.2f,
                strength: 12f,
                vibrato: 12,
                randomness: 90f,
                snapping: false,
                fadeOut: true
            )
        );
    }

    private IEnumerator RollDiceCoroutine(Action<int> onComplete)
    {
        float totalTime = 2.5f;
        float elapsed = 0f;
        float interval = 0.05f;

        StartShuffleBounceAnimation();

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

        StopShuffleBounceAnimation();

        int finalIndex = UnityEngine.Random.Range(0, diceDataSO.diceDatasList.Count);
        currentDiceData = diceDataSO.diceDatasList[finalIndex];

        if (diceImage != null)
        {
            diceImage.sprite = currentDiceData.diceSprite;
        }

        PlayDiceResultAnimation();

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
            Color color = diceImage.color;
            color.a = isVisible ? 1f : 0f;
            diceImage.color = color;
        }
    }

    public void InitializeDiceCanvasGroup()
    {
        if (bounceTween != null && bounceTween.IsActive())
        {
            bounceTween.Kill();
        }

        if (diceCanvasGroup != null)
        {
            diceCanvasGroup.alpha = 0f;
            diceCanvasGroup.interactable = false;
            diceCanvasGroup.blocksRaycasts = false;
        }

        if (diceRectTransform != null)
        {
            Vector2 pos = diceRectTransform.anchoredPosition;
            pos.y = defaultAnchoredPosY;
            diceRectTransform.anchoredPosition = pos;
            diceRectTransform.localScale = Vector3.one;
            diceRectTransform.localRotation = Quaternion.identity;
        }

        currentState = DiceState.Idle;
    }

    private void StartShuffleBounceAnimation()
    {
        if (diceRectTransform == null) return;

        // 既存Tween停止
        if (bounceTween != null && bounceTween.IsActive())
        {
            bounceTween.Kill();
        }

        // 念のため元の位置に戻す
        Vector2 pos = diceRectTransform.anchoredPosition;
        pos.y = defaultAnchoredPosY;
        diceRectTransform.anchoredPosition = pos;

        bounceTween = diceRectTransform
            .DOAnchorPosY(defaultAnchoredPosY + 250f, 0.3f)
            .SetEase(Ease.OutQuad)
            .SetLoops(-1, LoopType.Yoyo);

        diceRectTransform.DOAnchorPosY(defaultAnchoredPosY + 30f, 0.3f);
    }

    private void StopShuffleBounceAnimation()
    {
        if (diceRectTransform == null) return;

        if (bounceTween != null && bounceTween.IsActive())
        {
            bounceTween.Kill();
        }

        diceRectTransform.DOAnchorPosY(defaultAnchoredPosY, 0.1f)
            .SetEase(Ease.OutQuad);
    }
}