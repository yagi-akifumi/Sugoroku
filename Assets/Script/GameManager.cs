using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public enum GameState
{
    Idle,
    PlayerMoving
}

public class GameManager : MonoBehaviour
{
    public PlayerManager player;
    public UIManager uiManager;
    public List<Transform> tiles;

    private bool isMoving = false;
    public int currentTurn = 0;
    public int maxTurn = 12;

    public GameState currentState = GameState.Idle;
    [SerializeField]
    private StatusGenerator statusGenerator;


    public void Start()
    {
        uiManager.SetUpUIManager();
        statusGenerator.SetUpStatusGenerator();
    }

    public void MoveSteps(int step)
    {
        if (isMoving) return;
        if (currentTurn >= maxTurn)
        {
            uiManager.SetDiceButtonInteractable(true);
            uiManager.SetDiceVisible(true);
            return;
        }
        currentTurn++;
        
        StartCoroutine(MoveCoroutine(step));


    }

    private IEnumerator MoveCoroutine(int step)
    {
        isMoving = true;
        currentState = GameState.PlayerMoving;

        uiManager.SetDiceButtonInteractable(false);

        for (int i = 0; i < step; i++)
        {
            player.currentIndex = (player.currentIndex + 1) % tiles.Count;

            Vector3 targetPos = tiles[player.currentIndex].position;

            yield return player.transform
                .DOJump(targetPos, 0.5f, 1, 0.3f)
                .SetEase(Ease.OutQuad)
                .WaitForCompletion();
        }

        isMoving = false;
        currentState = GameState.Idle;

        uiManager.OnCountDiceTurn();
        uiManager.SetDiceButtonInteractable(true);
        uiManager.InitializeDice();
    }
}