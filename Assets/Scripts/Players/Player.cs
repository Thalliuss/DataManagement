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

    public void LoadAssets(int id)
    {
        _database = Main.Instance.database;

        if (File.Exists(Application.dataPath + "/Resources/" + name + ".json"))
        {
            JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/Resources/" + name + ".json"), _database.playerInfo[id]);
            playerInfo = _database.playerInfo[id];
            image.sprite = playerInfo.race.sprite;

            return;
        }
        playerInfo = _database.playerInfo[id];
        image.sprite = playerInfo.race.sprite;
    }

    private void Update()
    {
        if (playerInfo != null) playerInfo.armor.CalculatePoints();
    }
}
