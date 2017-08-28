#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

using System;
using System.IO;
using System.Collections;

namespace DataManagement
{
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

        [Header("Enable/Disable Multiple Save Files."), SerializeField]
        private bool multipleSaves;

        public DataReferences DataReferences
        {
            get
            {
                return _dataReferences;
            }
        }
        [Header("Data."), SerializeField]
        private DataReferences _dataReferences;
        [SerializeField] private SaveReferences _saveReferences;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _dataReferences.initialID = _dataReferences.ID;

            var _path = Application.persistentDataPath + "/" + _dataReferences.ID + "/";
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            if (_instance != null)
                Destroy(gameObject);

            _instance = this;

            Build();

            if (!multipleSaves)
            {
                if (_saveReferences.save != null)
                    _saveReferences.save.gameObject.SetActive(false);

                if (_saveReferences.load != null)
                    _saveReferences.load.gameObject.SetActive(false);
            }
            else _saveReferences.Init();
        }

        private void Build()
        {
            #if UNITY_EDITOR
            if (!Directory.Exists("Assets/Temp_Assets/"))
                Directory.CreateDirectory("Assets/Temp_Assets/");

            AssetDatabase.Refresh();
            #endif

            DataBuilder.BuildDataReferences();

            DataBuilder.BuildElementsOfType<Account>(_dataReferences.SaveData);
            DataBuilder.BuildElementsOfType<Character>(_dataReferences.SaveData);
            DataBuilder.BuildElementsOfType<Class>(_dataReferences.SaveData);
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _dataReferences.SaveData.info.Count; i++)
                _dataReferences.SaveData.info[i].Destroy();

            _dataReferences.SaveData.ids.Clear();
            _dataReferences.SaveData.info.Clear();
            _dataReferences.SaveData.types.Clear();
            _dataReferences.ID = _dataReferences.initialID;

            #if UNITY_EDITOR
            if (Directory.Exists("Assets/Temp_Assets/"))
                FileUtil.DeleteFileOrDirectory("Assets/Temp_Assets/");

            AssetDatabase.Refresh();
            #endif
        }

        [ContextMenu("Manual Save.")]
        public void Save()
        {
            if (multipleSaves)
            {
                var _time = DateTime.Now.ToString();

                _time = _time.Replace('/', '-');
                _time = _time.Replace(' ', '_');
                _time = _time.Replace(':', '-');

                var _path = Application.persistentDataPath + "/";
                if (Directory.Exists(_path + _dataReferences.initialID + "/"))
                {
                    Directory.CreateDirectory(_path + _dataReferences.initialID + "_" + _time);

                    for (int i = 0; i < Directory.GetFiles(_path + _dataReferences.ID).Length; i++)
                        File.Copy(Directory.GetFiles(_path + _dataReferences.ID)[i], Directory.GetFiles(_path + _dataReferences.ID)[i].Replace(_dataReferences.ID, _dataReferences.initialID + "_" + _time));
                    
                    _saveReferences.Init();
                    _dataReferences.ID = _dataReferences.initialID + "_" + _time;
                }
            }
        }

        public void Load()
        {
            if (multipleSaves)
            {
                _dataReferences.ID = _saveReferences.saveData[_saveReferences.load.value];
                _dataReferences.Save();

                _dataReferences.SaveData.ids.Clear();
                _dataReferences.SaveData.info.Clear();
                _dataReferences.SaveData.types.Clear();

                #if UNITY_EDITOR
                if (Directory.Exists("Assets/Temp_Assets/"))
                    FileUtil.DeleteFileOrDirectory("Assets/Temp_Assets/");

                AssetDatabase.Refresh();
                #endif

                Build();
            }
        }

        [ContextMenu("Clear Data.")]
        public void ClearData()
        {
            var _path = Application.persistentDataPath + "/";
            var _data = Directory.GetDirectories(_path);
            for (int i = 0; i < _data.Length; i++)
            {
                if (_data[i].Contains(_dataReferences.ID))
                {
                    #if UNITY_EDITOR
                    if (Directory.Exists(_data[i]))
                    {
                        FileUtil.DeleteFileOrDirectory(_data[i]);
                        Debug.Log("Succesfully cleaned all saved data...");
                    }
                    AssetDatabase.Refresh();
                    #endif
                }
            }
        }

        [ContextMenu("Clear PlayerPrefs.")]
        public void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
