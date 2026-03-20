using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public PlayerManager player;
    public List<Transform> tiles;

    private bool isMoving = false;

    public void MoveSteps(int step)
    {
        if (isMoving) return;
        StartCoroutine(MoveCoroutine(step));
    }

    IEnumerator MoveCoroutine(int step)
    {
        isMoving = true;

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
    }
}