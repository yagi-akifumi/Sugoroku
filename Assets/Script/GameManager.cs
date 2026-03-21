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
    public List<Tile> tiles;

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
            int nextIndex = (player.currentIndex + 1) % tiles.Count;
            Tile nextTile = tiles[nextIndex];

            // 次に入るTileの向きでアニメ変更
            player.SetMoveAnimation(nextTile.moveDirection);

            player.currentIndex = nextIndex;

            Vector3 targetPos = nextTile.transform.position;

            yield return player.transform
                .DOJump(targetPos, 0.5f, 1, 0.3f)
                .SetEase(Ease.OutQuad)
                .WaitForCompletion();
        }

        // 止まったら正面向き待機
        player.SetIdleFront();

        isMoving = false;
        currentState = GameState.Idle;

        uiManager.OnCountDiceTurn();
        uiManager.SetDiceButtonInteractable(true);
        uiManager.InitializeDice();

        // 将来UTAGEイベントに使える
        Tile currentTile = tiles[player.currentIndex];
        Debug.Log("停止マス: " + currentTile.index + " / eventId: " + currentTile.eventId);
    }
}