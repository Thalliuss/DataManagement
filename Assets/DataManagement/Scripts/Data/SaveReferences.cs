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

            var t_path = Application.persistentDataPath + "/";
            var t_data = Directory.GetDirectories(t_path);
            for (int i = 0; i < t_data.Length; i++)
            {
                t_data[i] = t_data[i].Replace(t_path, "");

                if (t_data[i].Contains(DataManager.Instance.DataReferences.initialID))
                {
                    saveData.Add(t_data[i]);

                    if (t_data[i] != DataManager.Instance.DataReferences.initialID)
                        load.options.Add(new Dropdown.OptionData(t_data[i]));
                    
                    else load.options.Add(new Dropdown.OptionData(DataManager.Instance.DataReferences.initialID));
                }
            }
        }
    }
}