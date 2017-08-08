using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
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

    private DataBuilder _builder;

    [Header("Data.")] public Data data;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance == null)
            _instance = this;

        Build();
    }

    private void Build()
    {
        _builder = new DataBuilder(data);

        _builder.BuildData();

        // Add new DataElement inheritants to the for loop below.
        for (int i = 0; i < data.saveData.info.Count; i++)
        {
            if (data.saveData.types[i] == "Account") {
                _builder.BuildElement<Account>(data.saveData.info[i], i);
            } else _builder.BuildElement<DataElement>(data.saveData.info[i], i);
        }
    }

    private void OnDestroy()
    {
        data.saveData.ids.Clear();
        data.saveData.info.Clear();
        data.saveData.types.Clear();
    }
}
