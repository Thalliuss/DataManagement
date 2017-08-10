using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu]
public class Data : ScriptableObject
{
    [Serializable]
    public class SaveData
    {
        public List<string> ids = new List<string>();
        public List<string> types = new List<string>();
        public List<DataElement> info = new List<DataElement>();
    }
    public SaveData saveData;

    public void AddElement<T>(DataElement element) where T : DataElement
    {
        T _info = (T)DataParser.CreateAsset<T>(element.Id);

        SaveJSON(element.Id, JsonUtility.ToJson(element));
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/Resources/" + element.Id + ".json"), _info);

        saveData.info.Add(_info);
        saveData.types.Add(_info.GetType().ToString());

        Update();
    }

    public DataElement FindElement(string id)
    {
        for (int i = 0; i < saveData.ids.Count; i++)
        {
            if (saveData.ids[i] == id)
                return saveData.info[i];
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

    public void Update()
    {
        SaveJSON(this.ToString(), JsonUtility.ToJson(this));
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/Resources/" + this.ToString() + ".json"), this);
    }
}

