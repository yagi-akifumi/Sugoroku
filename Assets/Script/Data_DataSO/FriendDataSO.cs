using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FriendDataSO", menuName = "Create FriendDataSO")]
public class FriendDataSO : ScriptableObject
{
    public List<FriendData> friendDatasList = new List<FriendData>();
}
