using UnityEngine;
using System.IO;

public class DataBuilder
{
    private Data _data;

    public DataBuilder(Data data)
    {
        _data = data;
    }

    public void BuildData()
    {
        var _path = Application.dataPath + "/Resources/";

        if (File.Exists(_path + _data.ToString() + ".json"))
            JsonUtility.FromJsonOverwrite(File.ReadAllText(_path + _data.ToString() + ".json"), _data);

        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    public void BuildElement<T>(DataElement element, int index) where T : ScriptableObject
    {
        var _path = Application.dataPath + "/Resources/";
        var _id = _data.saveData.ids[index].ToString();

        if (File.Exists(_path + _id + ".json")) {
            element = (DataElement)DataParser.CreateAsset<T>(_id);
            JsonUtility.FromJsonOverwrite(File.ReadAllText(_path + _id + ".json"), element);
        }
        _data.saveData.info[index] = element;
        _data.Update();

        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }
}
