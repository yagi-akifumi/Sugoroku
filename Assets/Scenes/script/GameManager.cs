using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    
        public PlayerManager player;
        public List<Transform> tiles;

        public void MoveOneStep()
        {
            player.currentIndex = (player.currentIndex + 1) % tiles.Count;
            player.transform.position = tiles[player.currentIndex].position;
        }
}
