using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

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
}
