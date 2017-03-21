using System.IO;
using UnityEngine;

public static class DatabaseHelper
{
    public static string AddPlayerInfo(Database database, string name, int race, int profession, int weapon)
    {
        if (name != null)
        {
            var length = database.playerInfo.Count;
            for (var i = 0; i < length; i++)
                if (name == database.playerInfo[i].name)
                    return "Name already taken...";

            var info = (PlayerInfo)ScriptableObjectHelper.CreateAsset<PlayerInfo>(name, "Assets/Scripts/Players");

            database.playerInfo.Add(info);

            var current = database.playerInfo.Count - 1;

            database.playerInfo[current].name = name;
            database.playerNames.Add(name);

            database.playerInfo[current].race = database.races[race];
            database.playerInfo[current].profession = database.professions[profession];
            database.playerInfo[current].armor = database.armor;
            database.playerInfo[current].weapon = database.weapons[weapon];

            SaveJSON(name, JsonUtility.ToJson(database.playerInfo[current]));
            SaveJSON("database", JsonUtility.ToJson(database));

            return name;
        }
        return null;
    }

    public static void SaveJSON(string name, string info)
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
