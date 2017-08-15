using UnityEngine;
using System.IO;
using System;

public abstract class Constructor<T> : ScriptableObject
{
    public Constructor(T id) { }
}
public class DataElement : Constructor<string>
{
    private DataManager _dataManager;
    private DataBuilder _builder;

    private void Awake()
    {
        _dataManager = DataManager.Instance;
    }

    public string ID
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }
    [Header("Element's ID:"), SerializeField]
    private string _id;

    [Header("Element's SaveData:")]
    public Data.SaveData saveData;

    public DataElement(string id) : base(id)
    {
        _id = ID;
    }

    public void AddElement<T>(DataElement element) where T : DataElement
    {
        for (int i = 0; i < saveData.ids.Count; i++)
            if (element.ID == saveData.ids[i]) throw new ArgumentException("Argument already exists.");

        T _info = (T)DataParser.CreateAsset<T>(element.ID);

        DataParser.SaveJSON(element.ID, JsonUtility.ToJson(element));
		JsonUtility.FromJsonOverwrite(DataBuilder.Decrypt(File.ReadAllText(Application.persistentDataPath + "/SaveData/" + element.ID + ".json")), _info);

        saveData.ids.Add(element.ID);
        saveData.info.Add(_info);
        saveData.types.Add(_info.GetType().ToString());

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

    public void Build()
    {
        _builder = new DataBuilder(saveData);
        for (int i = 0; i < saveData.ids.Count; i++)
        {
            _builder.BuildElement<DataElement>(i);
            saveData.info[i].Build();
        }
    }

    public void Update()
    {
        DataParser.SaveJSON(_id.ToString(), JsonUtility.ToJson(this));
		JsonUtility.FromJsonOverwrite(DataBuilder.Decrypt(File.ReadAllText(Application.persistentDataPath + "/SaveData/" + _id.ToString() + ".json")), this);
    }

    public void Destroy()
    {
        for (int i = 0; i < saveData.ids.Count; i++)
			saveData.info[i].Destroy();

        saveData.ids.Clear();
        saveData.info.Clear();
        saveData.types.Clear();
    }
}

