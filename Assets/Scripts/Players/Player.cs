using Kevin.Database;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player Name.")]
    public string name = "kevin";
    public PlayerInfo playerInfo;

    [SerializeField] public Database _database;

    public Toggle toggle;

    private string path;

    private void Awake()
    {
        _database.Init();

        if (!Application.isEditor)
            path = Application.dataPath + "/Resources/" + name + ".json";
        else
            path = "Assets/Resources/GameJSONData/" + name + ".json";
    }

    private void Start()
    {
        if (Application.isEditor)
        {
            PlayerInfo result;
            if (_database.playerInfo.TryGetValue(name, out result))
            {
                if (result == null) return;

                var json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json.ToString(), _database.playerInfo[name]);

                playerInfo = _database.playerInfo[name];

                toggle.isOn = true;
            }
        }
        else
        {
            if (!File.Exists(path)) return;

            _database.AddPlayerInfo(name);

            var json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json.ToString(), _database.playerInfo[name]);

            playerInfo = _database.playerInfo[name];

            toggle.isOn = true;
        }
    }

    public void LoadPlayer()
    {
        PlayerInfo result;
        if (_database.playerInfo.TryGetValue(name, out result)) {
            if (result == null && Application.isEditor) return;

            var json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json.ToString(), _database.playerInfo[name]);
            
            playerInfo = _database.playerInfo[name];

            toggle.isOn = true;
        }
    }

    private void Update()
    {
        if (playerInfo != null) playerInfo.armor.CalculatePoints();
    }
}
