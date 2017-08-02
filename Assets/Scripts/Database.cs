using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu]
public class Database : ScriptableObject
{
    public List<Account> accounts = new List<Account>();

    public void AddElement<T>(IDatabaseElement element) where T : ScriptableObject
    {
        var _path = Application.dataPath + "/Resources/" + element.Id;
        var _info = ScriptableObjectHelper.CreateAsset<T>(element.Id, "Assets/Resources");

        SaveJSON(element.Id, JsonUtility.ToJson(element));
        JsonUtility.FromJsonOverwrite(File.ReadAllText(_path + ".json"), _info);
        SaveJSON(this.ToString(), JsonUtility.ToJson(this));
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/Resources/" + this.ToString() + ".json"), _info);
    }

    public void RemoveElement<T>(IDatabaseElement element) where T : ScriptableObject
    {
        var _path = Application.dataPath + "/Resources/" + element.ToString();

        var asset = _path + ".asset";
        if (File.Exists(asset))
            File.Delete(asset);

        var json = _path + ".json";
        if (File.Exists(json))
            File.Delete(json);

        var database = Application.dataPath + "/Resources/" + this.ToString() + ".json";
        if (File.Exists(database))
            File.Delete(database);
    }

    public IDatabaseElement FindElement<T>(List<T> list, string id) where T : IDatabaseElement
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Id == id)
                return list[i];
        }
        return null;
    }

    private void SaveJSON(string name, string info) 
    {
        var _path = Application.dataPath + "/Resources/" + name + ".json";

        using (FileStream fs = new FileStream(_path, FileMode.Create))
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

