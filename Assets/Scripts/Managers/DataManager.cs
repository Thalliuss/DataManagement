using System;
using System.Collections.Generic;
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

    public DataReferences DataReferences
    {
        get {
            return _dataReferences;
        }
    }
    [Header("Data."), SerializeField]
    private DataReferences _dataReferences;

    private void Awake()
    {
        var _path = Application.persistentDataPath + "/" + DataReferences.ID + "/";
        if (!Directory.Exists(_path))
			Directory.CreateDirectory (_path);

		if (_instance != null)
			Destroy (gameObject);

		_instance = this;

        Build();
    }

    private void Build()
    {
        DataBuilder.BuildDataReferences();

        for (int i = 0; i < _dataReferences.SaveData.info.Count; i++)
        {
			if (_dataReferences.SaveData.types [i] == "Account") {
				DataBuilder.BuildElement<Account>(_dataReferences.SaveData, i);
				_dataReferences.SaveData.info [i].Build ();
			}
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _dataReferences.SaveData.info.Count; i++)
            _dataReferences.SaveData.info[i].Destroy();

        _dataReferences.SaveData.ids.Clear();
        _dataReferences.SaveData.info.Clear();
        _dataReferences.SaveData.types.Clear();
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        var _path = Application.persistentDataPath + "/" + _dataReferences.ID + "/";

        #if UNITY_EDITOR
        if (Directory.Exists(_path)) {
            UnityEditor.FileUtil.DeleteFileOrDirectory(_path);
            Debug.Log("Succesfully cleaned all saved data...");
        }
        #endif
    }
}
