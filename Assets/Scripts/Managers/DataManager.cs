using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    private static DataManager _instance;
    public static DataManager Instance
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

    [Header("Enable/Disable Encryption.")]
    public bool encrypt;

    [Header("Data.")]
    public Data data;

    private DataBuilder _builder;

    private void Awake()
    {
		var _path = Application.persistentDataPath + "/Resources/";
		if (!Directory.Exists(_path))
			Directory.CreateDirectory (_path);

        if (_instance == null)
            _instance = this;

        Build();
    }

    private void Build()
    {
        _builder = new DataBuilder(data.saveData);

        _builder.BuildData();

        for (int i = 0; i < data.saveData.info.Count; i++)
        {
			if (data.saveData.types[i] == "Account") {
				_builder.BuildElement<Account>(i);
                data.saveData.info[i].Build();
			} 
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < data.saveData.info.Count; i++)
			data.saveData.info[i].Destroy();

        data.saveData.ids.Clear();
        data.saveData.info.Clear();
        data.saveData.types.Clear();
    }
}
