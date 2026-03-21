using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerVisual;
    public int currentIndex = 0;

    public void SetMoveAnimation(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Up:
                animator.SetFloat("X", 0f);
                animator.SetFloat("Y", 1f);
                break;

            case MoveDirection.Down:
                animator.SetFloat("X", 0f);
                animator.SetFloat("Y", -1f);
                break;

            case MoveDirection.Left:
                animator.SetFloat("X", -1f);
                animator.SetFloat("Y", 0f);
                break;

            case MoveDirection.Right:
                animator.SetFloat("X", 1f);
                animator.SetFloat("Y", 0f);
                break;
        }

        animator.SetBool("IsMoving", true);
    }

    public void SetIdleFront()
    {
        // 常に正面を向く
        animator.SetFloat("X", 0f);
        animator.SetFloat("Y", -1f);
        animator.SetBool("IsMoving", false);
    }

    public Tween PlayJumpVisual(float jumpPower, float duration)
    {
        if (playerVisual == null) return null;

        playerVisual.DOKill();
        playerVisual.localPosition = Vector3.zero;

        return playerVisual
            .DOLocalJump(Vector3.zero, jumpPower, 1, duration)
            .SetEase(Ease.OutQuad);
    }
}
