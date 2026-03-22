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

        if (SaveManager.HasSaveData())
        {
            LoadGame();
        }
        else
        {
            SyncToGameData();
        }
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

            player.SetMoveAnimation(nextTile.moveDirection);
            player.currentIndex = nextIndex;

            Vector3 targetPos = nextTile.transform.position;

            Tween moveTween = player.transform
                .DOMove(targetPos, 0.3f)
                .SetEase(Ease.Linear);

            player.PlayJumpVisual(0.5f, 0.3f);

            yield return moveTween.WaitForCompletion();
        }

        player.SetIdleFront();

        isMoving = false;
        currentState = GameState.Idle;

        SyncToGameData();

        uiManager.OnCountDiceTurn();
        uiManager.SetDiceButtonInteractable(true);
        uiManager.InitializeDice();

        Tile currentTile = tiles[player.currentIndex];
        Debug.Log("停止マス: " + currentTile.index + " / eventId: " + currentTile.eventId);

        SaveGame();
    }

    public void SaveGame()
    {
        SyncToGameData();

        SaveData saveData = new SaveData();

        saveData.isSaveData = true;
        saveData.playerName = "Player";

        saveData.currentTurn = GameData.instance.currentTurn;
        saveData.playerIndex = GameData.instance.playerIndex;

        saveData.life = GameData.instance.life;
        saveData.power = GameData.instance.power;
        saveData.intelligence = GameData.instance.intelligence;
        saveData.guts = GameData.instance.guts;
        saveData.coolness = GameData.instance.coolness;
        saveData.morality = GameData.instance.morality;
        saveData.kindness = GameData.instance.kindness;

        SaveManager.Save(saveData);
    }

    public void LoadGame()
    {
        SaveData saveData = SaveManager.Load();

        if (saveData == null)
        {
            Debug.Log("ロードできるデータがありません");
            return;
        }

        if (saveData.playerIndex < 0 || saveData.playerIndex >= tiles.Count)
        {
            Debug.LogWarning("保存された playerIndex が不正です");
            return;
        }

        GameData.instance.isSaveData = saveData.isSaveData;

        GameData.instance.currentTurn = saveData.currentTurn;
        GameData.instance.playerIndex = saveData.playerIndex;

        GameData.instance.life = saveData.life;
        GameData.instance.power = saveData.power;
        GameData.instance.intelligence = saveData.intelligence;
        GameData.instance.guts = saveData.guts;
        GameData.instance.coolness = saveData.coolness;
        GameData.instance.morality = saveData.morality;
        GameData.instance.kindness = saveData.kindness;

        ApplyFromGameData();

        player.SetIdleFront();
        currentState = GameState.Idle;
        isMoving = false;

        uiManager.OnCountDiceTurn();
        uiManager.InitializeDice();

        Debug.Log("ゲームデータを反映しました");
    }

    private void SyncToGameData()
    {
        GameData.instance.currentTurn = currentTurn;
        GameData.instance.playerIndex = player.currentIndex;
    }

    private void ApplyFromGameData()
    {
        currentTurn = GameData.instance.currentTurn;
        player.currentIndex = GameData.instance.playerIndex;

        if (tiles != null && player.currentIndex >= 0 && player.currentIndex < tiles.Count)
        {
            player.transform.position = tiles[player.currentIndex].transform.position;
        }
    }
}