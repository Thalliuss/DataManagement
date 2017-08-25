﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace DataManagement
{
    public static class DataParser
    {
        public static string Encrypt(string s)
        {
            if (DataManager.Instance.encrypt)
            {
                byte[] inputbuffer = Encoding.Unicode.GetBytes(s);
                byte[] outputBuffer = DES.Create().CreateEncryptor(DataReferences.key, DataReferences.iv).TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
                return System.Convert.ToBase64String(outputBuffer);
            }
            else return s;
        }

        public static ScriptableObject CreateAsset<T>(string name) where T : ScriptableObject
        {
            var _path = Application.persistentDataPath + "/" + DataManager.Instance.DataReferences.ID + "/" + name + ".asset";
            T _asset = ScriptableObject.CreateInstance<T>() as T;

            #if UNITY_EDITOR
            AssetDatabase.CreateAsset(_asset, "Assets/Temp_Assets/" + name + ".asset");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = _asset;
            #endif

            return _asset;
        }

        public static void SaveJSON(string name, string info)
        {
            var _path = Application.persistentDataPath + "/" + DataManager.Instance.DataReferences.ID + "/" + name + ".json";
            if (!File.Exists(_path)) File.Delete(_path);

            using (FileStream fs = new FileStream(_path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                    writer.Write(Encrypt(info));
            }
        }
    }
}

