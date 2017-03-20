using System.IO;
using UnityEngine;

public static class DatabaseHelper
{

    public static void SaveJSON(string name, string info)
    {
        string path = null;

        if (!Application.isEditor)
            path = Application.dataPath + "/Resources/" + name + ".json";
        else
            path = "Assets/Resources/GameJSONData/" + name + ".json";

        using (FileStream fs = new FileStream(path, FileMode.Create))
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
