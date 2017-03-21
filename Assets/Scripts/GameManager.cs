using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }

        set
        {
            _instance = value;
        }
    }

    [ContextMenu("Clear Database.")]
    public void ClearDatabase() { DatabaseHelper.RemovePlayerInfo(database); }

    [Header("Database.")] public Database database;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        Parse();
    }

    public void Parse()
    {
        if (File.Exists(Application.dataPath + "/Resources/database.json"))
            JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/Resources/database.json"), database);

        for (var i = 0; i < database.playerInfo.Count; i++) {
            if (File.Exists(Application.dataPath + "/Resources/" + database.playerNames[i] + ".json")) {
                database.playerInfo[i] = new PlayerInfo();
                JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/Resources/" + database.playerNames[i] + ".json"), database.playerInfo[i]);
            }
        }
    }
}
