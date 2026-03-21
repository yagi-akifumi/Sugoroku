using UnityEngine;


public enum MoveDirection
{
    Up,
    Down,
    Left,
    Right
}

public class Tile : MonoBehaviour
{
    public int index;
    public string eventId;
    public MoveDirection moveDirection;
}
