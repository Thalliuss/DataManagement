using Kevin.Database;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("PlayerName.")]
    public string name;
    public PlayerInfo playerInfo;

    [SerializeField, Header("Database.")]
    private Database _database;

    private void Awake()
    {
        _database.Init();
    }

    private void Start()
    {
        playerInfo = _database.playerInfo[name];
    }

    private void Update()
    {
        playerInfo.armor.CalculatePoints();
    }
}
