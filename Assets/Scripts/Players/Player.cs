using Kevin.Database;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player Name.")]
    public string name = "kevin";
    public PlayerInfo playerInfo;

    private Database _database;

    public Toggle toggle;
    public Image image;

    private void Start()
    {
        _database = Main.Instance.database;

        if (Application.isEditor)
        {
            PlayerInfo result;
            if (_database.playerInfo.TryGetValue(name, out result))
            {
                if (result == null) return;

                JsonUtility.FromJsonOverwrite(File.ReadAllText("Assets/Resources/GameJSONData/" + name + ".json"), _database.playerInfo[name]);
                DatabaseHelper.SaveJSON("database", JsonUtility.ToJson(_database));

                playerInfo = _database.playerInfo[name];

                toggle.isOn = true;
                image.sprite = playerInfo.race.sprite;
            }
        }
        else
        {
            if (!File.Exists(Application.dataPath + "/Resources/" + name + ".json")) return;

            _database.AddPlayerInfo(name);

            JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/Resources/" + name + ".json"), _database.playerInfo[name]);
            DatabaseHelper.SaveJSON("database", JsonUtility.ToJson(_database));

            playerInfo = _database.playerInfo[name];

            toggle.isOn = true;
            image.sprite = playerInfo.race.sprite;
        }
    }

    public void LoadPlayer()
    {
        PlayerInfo result;
        if (_database.playerInfo.TryGetValue(name, out result)) {
            if (result == null && Application.isEditor) return;
            
            playerInfo = _database.playerInfo[name];

            toggle.isOn = true;
            image.sprite = playerInfo.race.sprite;
        }
    }

    private void Update()
    {
        if (playerInfo != null) playerInfo.armor.CalculatePoints();
    }
}
