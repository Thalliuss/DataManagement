using System.Collections.Generic;
using System.IO;
using UnityEngine;


[CreateAssetMenu]
public class Database : ScriptableObject
{
    [Header("Player's")]
    public List<PlayerInfo> playerInfo = new List<PlayerInfo>();

    [SerializeField, Header("Race's")]
    private List<Race> _races = new List<Race>();

    [SerializeField, Header("Profession's")]
    private List<Profession> _professions = new List<Profession>();

    [SerializeField, Header("Armor set")]
    private Armor _armor;

    [SerializeField, Header("Weapon's.")]
    private List<Weapon> _weapons = new List<Weapon>();

    public void AddPlayerInfo(string name, int race, int profession, int weapon)
    {
        if (name != null)
        {
            var length = playerInfo.Count;
            for (var i = 0; i < length; i++)
                if (name == playerInfo[i].name) {
                    Debug.Log("Sorry, this name is already taken.");
                    return;
                } 

            var info = (PlayerInfo)ScriptableObjectHelper.CreateAsset<PlayerInfo>(name, "Assets/Scripts/Players");

            playerInfo.Add(info);

            var current = playerInfo.Count - 1;

            playerInfo[current].name = name;
            playerInfo[current].race = _races[race];
            playerInfo[current].profession = _professions[profession];
            playerInfo[current].armor = _armor;
            playerInfo[current].weapon = _weapons[weapon];

            SaveJSON(name, JsonUtility.ToJson(playerInfo[current]));
            SaveJSON("database", JsonUtility.ToJson(this));
        }
    }
    public void AddPlayerInfo(string name)
    {
        if (name != null)
        {
            var length = playerInfo.Count;
            for (var i = 0; i < length; i++)
                if (name == playerInfo[i].name)
                {
                    Debug.Log("Sorry, this name is already taken.");
                    return;
                }

            var info = (PlayerInfo)ScriptableObjectHelper.CreateAsset<PlayerInfo>(name, "Assets/Scripts/Players");

            playerInfo.Add(info);

            var current = playerInfo.Count - 1;

            playerInfo[current].name = name;
        }
    }

    public PlayerInfo FindPlayerInfo(string name)
    {
        for (var i = 0; i < playerInfo.Count; i++)
            if (playerInfo[i].name == name) return playerInfo[i];

        return null;
    }

    public void SaveJSON(string name, string info)
    {
        string path = null;

        if (!Application.isEditor)
            path = Application.dataPath + "/Resources/" + name + ".json";
        else
            path = "Assets/Resources/GameJSONData/" + name + ".json";

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(info);
            }
        }
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }
}

