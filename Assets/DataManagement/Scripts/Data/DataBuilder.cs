﻿using UnityEngine;

using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DataManagement
{
    public static class DataBuilder
    {
        public static string Decrypt(string p_input)
        {
            if (DataManager.Instance.encrypt)
            {
                var t_inputbuffer = System.Convert.FromBase64String(p_input);
                var t_outputBuffer = DES.Create().CreateDecryptor(DataReferences.key, DataReferences.iv).TransformFinalBlock(t_inputbuffer, 0, t_inputbuffer.Length);
                return Encoding.Unicode.GetString(t_outputBuffer);
            }
            else return p_input;
        }

        public static void BuildDataReferences()
        {
            var t_dataManager = DataManager.Instance;
            var t_path = Application.persistentDataPath + "/" + t_dataManager.DataReferences.ID + "/" + t_dataManager.DataReferences.ID + ".json";

            if (File.Exists(t_path))
            {
                JsonUtility.FromJsonOverwrite(Decrypt(File.ReadAllText(t_path)), t_dataManager.DataReferences);
                Debug.Log("Building Data from: " + Application.persistentDataPath + "/" + t_dataManager.DataReferences.ID);
            }
        }

        public static void BuildElementsOfType<T>(DataReferences.SavedElement p_saveData) where T : DataElement
        {
            for (var i = 0; i < p_saveData.ids.Count; i++)
            {
                if (p_saveData.types[i] == typeof(T).Name)
                    BuildElementOfType<T>(p_saveData, i);

                p_saveData.info[i].Build<T>();
            }
        }

        public static void BuildElementOfType<T>(DataReferences.SavedElement p_saveData, int p_index) where T : DataElement
        {
            var t_id = p_saveData.ids[p_index].ToString();
            var t_path = Application.persistentDataPath + "/" + DataManager.Instance.DataReferences.ID + "/" + t_id + ".json";

            if (File.Exists(t_path))
            {
                var t_element = DataParser.CreateAsset<T>(t_id) as T;
                JsonUtility.FromJsonOverwrite(Decrypt(File.ReadAllText(t_path)), t_element);

                p_saveData.info[p_index] = t_element as T;
            }
        }
    }
}
