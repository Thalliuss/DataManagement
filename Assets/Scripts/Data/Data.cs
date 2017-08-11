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

        DataParser.SaveJSON(element.Id, JsonUtility.ToJson(element));
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/Resources/" + element.Id + ".json"), _info);

        saveData.info.Add(_info);
        saveData.types.Add(_info.GetType().ToString());

        Update();
    }

    public DataElement FindElement<T>(string id) where T : DataElement
    {
        for (int i = 0; i < saveData.ids.Count; i++)
        {
            if (saveData.ids[i] == id)
                return (T)saveData.info[i];
        }
        return null;
    }

    public void Update()
    {
        DataParser.SaveJSON(this.ToString(), JsonUtility.ToJson(this));
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/Resources/" + this.ToString() + ".json"), this);
    }
}



