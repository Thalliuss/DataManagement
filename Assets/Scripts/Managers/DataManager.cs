using System;
using System.IO;
using UnityEditor;
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

    [Header("Data.")] public Data data;

    private DataBuilder _builder;

    private void Awake()
    {
        DontDestroyOnLoad(transform);

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
        for (int i = 0; i < data.saveData.info.Count; i++)
        {
            var _path = "Assets/Elements/" + data.saveData.ids[i] + ".asset";
            if (File.Exists(_path)) { File.Delete(_path); }
        }

        data.saveData.ids.Clear();
        data.saveData.info.Clear();
        data.saveData.types.Clear();

        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }
}
