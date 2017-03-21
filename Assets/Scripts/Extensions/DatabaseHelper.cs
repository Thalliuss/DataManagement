using System;
using System.IO;
using UnityEngine;

public static class DatabaseHelper
{
    public static string AddPlayerInfo(Database database, string name, int race, int profession, int weapon)
    {
        if (name != "")
        {
            var length = database.playerInfo.Count;
            for (var i = 0; i < length; i++)
                if (name == database.playerInfo[i].name)
                    return "Name already taken...";

            var info = (PlayerInfo)ScriptableObjectHelper.CreateAsset<PlayerInfo>(name, "Assets/Scripts/Players");

            database.playerInfo.Add(info);

            var current = database.playerInfo.Count - 1;

            database.playerInfo[current].name = name;
            database.playerInfo[current].race = database.races[race];
            database.playerInfo[current].profession = database.professions[profession];
            database.playerInfo[current].armor = database.armor;
            database.playerInfo[current].weapon = database.weapons[weapon];

            database.playerNames.Add(name);

            SaveJSON(name, JsonUtility.ToJson(database.playerInfo[current]));
            SaveJSON("database", JsonUtility.ToJson(database));

            return name;
        }
        return "Name can not be emty...";
    }

    internal static PlayerInfo LoadPlayerInfo(Database database, string name)
    {
        for (var i = 0; i < database.playerInfo.Count; i++)
            if (database.playerInfo[i].name == name) return database.playerInfo[i];

        return null;
    }

    // For Editor use only!!!
    public static void RemovePlayerInfo(Database database)
    {
        for (var i = 0; i < database.playerNames.Count; i++)
        {
            var asset = "Assets/Scripts/Players/" + database.playerNames[i] + ".asset";
            if (File.Exists(asset))
                File.Delete(asset);

            var json = "Assets/Resources/" + database.playerNames[i] + ".json";
            if (File.Exists(json))
                File.Delete(json);
        }
        var db = "Assets/Resources/database.json";
        if (File.Exists(db))
            File.Delete(db);

        database.playerInfo.Clear();
        database.playerNames.Clear();

        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    private static void SaveJSON(string name, string info)
    {
        string path = null;

        if (!Application.isEditor)
            path = Application.dataPath + "/Resources/" + name + ".json";
        else
            path = "Assets/Resources/" + name + ".json";

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
