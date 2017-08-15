using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu]
public class Data : ScriptableObject
{
	public static byte[] key = new byte[8] {14, 43, 26, 54, 78, 107, 31, 65};
	public static byte[] iv = new byte[8] {10, 28, 20, 35, 88, 11, 7, 107};

	public string DataSaveID {
		get {
			return _dataSaveID;
		}
	}
	private const string _dataSaveID = "SAVE_DATA";


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
        for (int i = 0; i < saveData.ids.Count; i++)
            if (element.ID == saveData.ids[i]) throw new ArgumentException("Argument already exists.");

        T _info = (T)DataParser.CreateAsset<T>(element.ID);

        DataParser.SaveJSON(element.ID, JsonUtility.ToJson(element));
		JsonUtility.FromJsonOverwrite(DataBuilder.Decrypt(File.ReadAllText(Application.persistentDataPath + "/Resources/" + element.ID + ".json")), _info);

        saveData.ids.Add(element.ID);
        saveData.info.Add(_info);
        saveData.types.Add(_info.GetType().ToString());

        Update();
    }

	public void ReplaceElement<T>(DataElement element, int index) where T : DataElement
	{
		for (int i = 0; i < saveData.ids.Count; i++) 
		{
			if (element.ID == saveData.ids [i]) break; 
			else throw new ArgumentException ("Argument does not exists.");
		}

		T _info = (T)DataParser.CreateAsset<T>(element.ID);

		File.Delete (Application.persistentDataPath + "/Resources/" + element.ID + ".json");

		DataParser.SaveJSON(element.ID, JsonUtility.ToJson(element));
		JsonUtility.FromJsonOverwrite(DataBuilder.Decrypt(File.ReadAllText(Application.persistentDataPath + "/Resources/" + element.ID + ".json")), _info as T);

		saveData.ids[index] = element.ID;
		saveData.info[index] = _info;
		saveData.types[index] = _info.GetType().ToString();

		Update();
	}

	public T FindElement<T>(string id) where T : DataElement
    {
        for (int i = 0; i < saveData.ids.Count; i++)
        {
            if (saveData.ids[i] == id)
				return saveData.info[i] as T;
        }
        return null;
    }

    public void Update()
    {
		DataParser.SaveJSON(_dataSaveID, JsonUtility.ToJson(this));
		JsonUtility.FromJsonOverwrite(DataBuilder.Decrypt(File.ReadAllText(Application.persistentDataPath + "/Resources/" + _dataSaveID + ".json")), this);
    }
}



