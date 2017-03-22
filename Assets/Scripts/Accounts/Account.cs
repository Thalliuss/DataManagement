using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Account : ScriptableObject
{
    public string username;
    public string password;

    [Header("Player's.")]
    public List<string> characterNames = new List<string>();
    public List<PlayerInfo> characters = new List<PlayerInfo>();
}
