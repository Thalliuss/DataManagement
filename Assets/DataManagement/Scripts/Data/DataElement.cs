using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

namespace DataManagement
{
    public abstract class Singleton<T> : ScriptableObject
    {
        public Singleton(T id) { }
    }
    public class DataElement : Singleton<string>
    {
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

        public DataReferences.SavedElement SaveData
        {
            get
            {
                return _saveData;
            }

            set
            {
                _saveData = value;
            }
        }
        [Header("Element's SaveData:"), SerializeField]
        private DataReferences.SavedElement _saveData;

        public DataElement(string id) : base(id)
        {
            _id = ID;
        }

        public void AddElement<T>(DataElement element) where T : DataElement
        {
            for (int i = 0; i < _saveData.ids.Count; i++)
                if (element.ID == _saveData.ids[i]) return;

            T _info = (T)DataParser.CreateAsset<T>(element.ID);

            DataParser.SaveJSON(element.ID, JsonUtility.ToJson(element));
            JsonUtility.FromJsonOverwrite(DataBuilder.Decrypt(File.ReadAllText(Application.persistentDataPath + "/" + DataManager.Instance.DataReferences.ID + "/" + element.ID + ".json")), _info);

            _saveData.ids.Add(element.ID);
            _saveData.info.Add(_info);
            _saveData.types.Add(_info.GetType().ToString());

            Save();
        }

        public void ReplaceElement<T>(DataElement element, int index) where T : DataElement
        {
            for (int i = 0; i < _saveData.ids.Count; i++)
            {
                if (element.ID == _saveData.ids[index]) break;
                else throw new ArgumentException("Argument does not exists.");
            }

            T _info = (T)DataParser.CreateAsset<T>(element.ID);

            File.Delete(Application.persistentDataPath + "/" + DataManager.Instance.DataReferences.ID + "/" + element.ID + ".json");

            DataParser.SaveJSON(element.ID, JsonUtility.ToJson(element));
            JsonUtility.FromJsonOverwrite(DataBuilder.Decrypt(File.ReadAllText(Application.persistentDataPath + "/" + DataManager.Instance.DataReferences.ID + "/" + element.ID + ".json")), _info as T);

            _saveData.ids[index] = element.ID;
            _saveData.info[index] = _info;
            _saveData.types[index] = _info.GetType().ToString();

            Save();
        }

        public void RemoveElement<T>(string id) where T : DataElement
        {
            for (int i = 0; i < _saveData.ids.Count; i++)
            {
                if (_saveData.ids[i] == id && _saveData.types[i] == typeof(T).Name)
                {
                    Debug.Log("Removing " + typeof(T).Name + ": " + id);

                    _saveData.info.Remove(_saveData.info[i]);
                    _saveData.ids.Remove(_saveData.ids[i]);
                    _saveData.types.Remove(_saveData.types[i]);

                    File.Delete(Application.persistentDataPath + "/" + DataManager.Instance.DataReferences.ID + "/" + id + ".json");
                    Save();
                }
            }
        }

        public T FindElement<T>(string id) where T : DataElement
        {
            for (int i = 0; i < _saveData.ids.Count; i++)
            {
                if (_saveData.ids[i] == id)
                    return _saveData.info[i] as T;
            }
            return null;
        }

        public T FindElement<T>(int index) where T : DataElement
        {
            if (SaveData.types[index] == typeof(T).Name)
                return _saveData.info[index] as T;

            return null;
        }

        public List<T> FindElementsOfType<T>() where T : DataElement
        {
            var _temp = new List<T>();
            for (int i = 0; i < SaveData.ids.Count; i++)
            {
                if (SaveData.types[i] == typeof(T).Name)
                    _temp.Add(SaveData.info[i] as T);
            }
            _temp.Reverse();
            return _temp;
        }

        public void Build<T>() where T : DataElement
        {
            for (int i = 0; i < _saveData.ids.Count; i++)
            {
                if (_saveData.types[i] == typeof(T).Name)
                    DataBuilder.BuildElementOfType<T>(_saveData, i);

                if (_saveData.info[i] != null)
                {
                    for (int a = 0; a < _saveData.info[i].SaveData.ids.Count; a++)
                    {
                        if (_saveData.info[i].SaveData.types[a] == typeof(T).Name)
                            _saveData.info[i].Build<T>();
                    }
                }
            }
        }

        public void Save()
        {
            DataParser.SaveJSON(_id.ToString(), JsonUtility.ToJson(this));
            JsonUtility.FromJsonOverwrite(DataBuilder.Decrypt(File.ReadAllText(Application.persistentDataPath + "/" + DataManager.Instance.DataReferences.ID + "/" + _id.ToString() + ".json")), this);
        }

        public void Destroy()
        {
            _saveData.ids.Clear();
            _saveData.info.Clear();
            _saveData.types.Clear();
        }
    }
}

