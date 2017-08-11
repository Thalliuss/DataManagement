using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class DataParser
{
    internal static ScriptableObject CreateAsset<T>(string name) where T : ScriptableObject
    {
        var _path = "Assets/Elements/";

        T asset = ScriptableObject.CreateInstance<T>();

        #if UNITY_EDITOR
        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(_path + name + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
        #endif

        return asset;
    }

    internal static void SaveJSON(string name, string info)
    {
        var _path = Application.dataPath + "/Resources/" + name + ".json";

        using (FileStream fs = new FileStream(_path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(info);
            }
        }
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }
}

