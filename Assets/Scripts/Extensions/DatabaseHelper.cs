using System;
using System.IO;
using UnityEngine;

public static class DatabaseHelper
{
    public static int CurrentID;

    public static string AddCharacter(Database database, string name, int race, int profession, int weapon)
    {
        if (name != "")
        {
            var length = database.accounts[CurrentID].characters.Count;
            for (var i = 0; i < length; i++)
                if (name == database.accounts[CurrentID].characters[i].name)
                    return "Name already taken...";

            var info = (PlayerInfo)ScriptableObjectHelper.CreateAsset<PlayerInfo>(name, "Assets/Scripts/Players");

            Debug.Log(CurrentID);
            database.accounts[CurrentID].characters.Add(info);

            var current = database.accounts[CurrentID].characters.Count - 1;

            database.accounts[CurrentID].characters[current].name = name;
            database.accounts[CurrentID].characters[current].race = database.races[race];
            database.accounts[CurrentID].characters[current].profession = database.professions[profession];
            database.accounts[CurrentID].characters[current].armor = database.armor;
            database.accounts[CurrentID].characters[current].weapon = database.weapons[weapon];

            database.accounts[CurrentID].characterNames.Add(name);

            SaveJSON(name, JsonUtility.ToJson(database.accounts[CurrentID].characters[current]));
            SaveJSON(database.usernames[CurrentID] + "_account", JsonUtility.ToJson(database.accounts[CurrentID]));
            SaveJSON("database", JsonUtility.ToJson(database));

            return name;
        }
        return "Name can not be emty...";
    }

    public static Account Login(Database database, string username, string password)
    {
        for (var i = 0; i < database.accounts.Count; i++)
            if (database.accounts[i].username == username && database.accounts[i].password == password) {
                CurrentID = i;
                SaveJSON(username + "_account", JsonUtility.ToJson(database.accounts[i]));
                SaveJSON("database", JsonUtility.ToJson(database));
                return database.accounts[i];
            }

        return null;
    }


    internal static PlayerInfo LoadCharacter(Database database, string name)
    {
        for (var i = 0; i < database.accounts[CurrentID].characters.Count; i++)
            if (database.accounts[CurrentID].characterNames[i] == name) return database.accounts[CurrentID].characters[i];

        return null;
    }

    // For Editor use only!!!
    public static void RemoveCharacters(Database database)
    {
        for (var i = 0; i < database.accounts.Count; i++)
        {
            for (var a = 0; a < database.accounts[i].characterNames.Count; a++)
            {
                var asset = "Assets/Scripts/Players/" + database.accounts[i].characterNames[a] + ".asset";
                if (File.Exists(asset))
                    File.Delete(asset);

                var json = "Assets/Resources/" + database.accounts[i].characterNames[a] + ".json";
                if (File.Exists(json))
                    File.Delete(json);
            }
            var account = "Assets/Resources/" + database.usernames[i] + "_account.json";
            if (File.Exists(account))
                File.Delete(account);
        }

        var db = "Assets/Resources/database.json";
        if (File.Exists(db))
            File.Delete(db);

        database.accounts[CurrentID].characters.Clear();
        database.accounts[CurrentID].characterNames.Clear();

        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    public static void SaveJSON(string name, string info)
    {
        string path = Application.dataPath + "/Resources/" + name + ".json";

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
