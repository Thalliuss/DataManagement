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
    public void ClearDatabase() { DatabaseHelper.RemoveCharacters(database); }

    [Header("Database.")] public Database database;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance == null)
            _instance = this;

        Parse();
    }

    public void Parse()
    {
        if (File.Exists(Application.dataPath + "/Resources/database.json"))
            JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/Resources/database.json"), database);

        for (var i = 0; i < database.accounts.Count; i++)
        {
            if (File.Exists(Application.dataPath + "/Resources/" + database.usernames[i] + "_account.json"))
            {
                JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/Resources/" + database.usernames[i] + "_account.json"), database.accounts[i]);

                for (var a = 0; a < database.accounts[i].characterNames.Count; a++)
                {
                    if (File.Exists(Application.dataPath + "/Resources/" + database.accounts[i].characterNames[a] + ".json"))
                    {
                        if (!Application.isEditor)
                            database.accounts[i].characters[a] = new PlayerInfo();

                        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/Resources/" + database.accounts[i].characterNames[a] + ".json"), database.accounts[i].characters[a]);
                    }
                }
            }
        }
    }
}
