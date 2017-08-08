using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class DataParser
{
    public static ScriptableObject CreateAsset<T>(string name) where T : ScriptableObject
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
}

