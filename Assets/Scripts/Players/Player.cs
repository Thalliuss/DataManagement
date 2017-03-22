using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player.")]
    public string name;
    public PlayerInfo playerInfo;

    private Database _database;

    public Image image;

    private void Start()
    {
        _database = GameManager.Instance.database;
    
        LoadAssets();
    }

    public void LoadAssets()
    {
        playerInfo = DatabaseHelper.LoadCharacter(_database, name);
        image.sprite = playerInfo.race.sprite;
    }

    private void Update()
    {
        if (playerInfo != null) playerInfo.armor.CalculatePoints();
    }
}
