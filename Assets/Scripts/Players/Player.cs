using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player Name.")]
    public string name = "kevin";
    public PlayerInfo playerInfo;

    private Database _database;

    public Image image;

    private void Start()
    {
        _database = Main.Instance.database;
    }

    private void Update()
    {
        if (playerInfo != null) playerInfo.armor.CalculatePoints();

        if (Application.isEditor)
        {
            if (_database.FindPlayerInfo(name) != null)
            {
                JsonUtility.FromJsonOverwrite(File.ReadAllText("Assets/Resources/GameJSONData/" + name + ".json"), _database.FindPlayerInfo(name));
                _database.SaveJSON("database", JsonUtility.ToJson(_database));

                playerInfo = _database.FindPlayerInfo(name);

                image.sprite = playerInfo.race.sprite;
            }
        }
        else
        {
            if (!File.Exists(Application.dataPath + "/Resources/" + name + ".json")) return;

            _database.AddPlayerInfo(name);

            JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/Resources/" + name + ".json"), _database.FindPlayerInfo(name));
            _database.SaveJSON("database", JsonUtility.ToJson(_database));

            playerInfo = _database.FindPlayerInfo(name);

            image.sprite = playerInfo.race.sprite;
        }
    }
}
