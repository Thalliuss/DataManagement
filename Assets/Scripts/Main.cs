using System.IO;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main _instance;
    public static Main Instance
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

    public Database database;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        if (File.Exists(Application.dataPath + "/Resources/database.json"))
            JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/Resources/database.json"), database);
    }
}
