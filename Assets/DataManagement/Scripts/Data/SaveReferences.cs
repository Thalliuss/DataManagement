using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;
using System.IO;

namespace DataManagement
{
    [Serializable]
    public class SaveReferences
    {
        public Dropdown load;
        public Button save;
        public List<string> saveData = new List<string>();

        public void Init()
        {
            if (saveData != null) saveData.Clear();
            if (load.options != null) load.options.Clear();

            var _path = Application.persistentDataPath + "/";
            var _data = Directory.GetDirectories(_path);
            for (int i = 0; i < _data.Length; i++)
            {
                _data[i] = _data[i].Replace(_path, "");

                if (_data[i].Contains(DataManager.Instance.DataReferences.initialID))
                {
                    saveData.Add(_data[i]);

                    if (_data[i] != DataManager.Instance.DataReferences.initialID)
                        load.options.Add(new Dropdown.OptionData(_data[i]));
                    
                    else load.options.Add(new Dropdown.OptionData(DataManager.Instance.DataReferences.initialID));
                }
            }
        }
    }
}