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

    [Header("Database.")] public Database database;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance == null)
            _instance = this;

        Parse();
    }

    private void OnDestroy()
    {
        database.accountsInfo.Clear();

        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    private void Parse()
    {
        var _path = Application.dataPath + "/Resources/";

        if (File.Exists(_path + database.ToString() + ".json"))
            JsonUtility.FromJsonOverwrite(File.ReadAllText(_path + database.ToString() + ".json"), database);

        for (int i = 0; i < database.accountsInfo.ids.Count; i++)
        {
            if(!File.Exists("Assets/Resources" + database.accountsInfo.ids[i].ToString() + ".asset"))
                database.accountsInfo.accounts[i] = (Account)ScriptableObjectHelper.CreateAsset<Account>(database.accountsInfo.ids[i], "Assets/Resources"); ;

            JsonUtility.FromJsonOverwrite(File.ReadAllText(_path + database.accountsInfo.ids[i] + ".json"), database.accountsInfo.accounts[i]);            
        }
       
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }
}
