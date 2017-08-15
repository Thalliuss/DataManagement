using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class DataBuilder
{
    private Data.SaveData _data;
    private DataManager _dataManager;

    public DataBuilder(Data.SaveData data)
    {
        _data = data;
        _dataManager = DataManager.Instance;
    }

	public static string Decrypt(string s) 
	{
		if (DataManager.Instance.encrypt) {
			byte[] inputbuffer = System.Convert.FromBase64String (s);
			byte[] outputBuffer = DES.Create ().CreateDecryptor (Data.key, Data.iv).TransformFinalBlock (inputbuffer, 0, inputbuffer.Length);
			return Encoding.Unicode.GetString (outputBuffer);
		} else return s;
	}

    public void BuildData()
	{
		var _path = Application.persistentDataPath + "/Resources/" + _dataManager.data.DataSaveID + ".json";

		if (File.Exists (_path)) 
			JsonUtility.FromJsonOverwrite (Decrypt (File.ReadAllText (_path)), _dataManager.data);
	}

	public void BuildElement<T>(int index) where T : DataElement
    {
        var _id = _data.ids[index].ToString();
		var _path = Application.persistentDataPath + "/Resources/" + _id + ".json";

        if (File.Exists(_path)) {
			Debug.Log ("Building: " + _path);

			var _element = DataParser.CreateAsset<T> (_id) as T;
			JsonUtility.FromJsonOverwrite(Decrypt(File.ReadAllText(_path)), _element);

			_data.info[index] = _element;
        }
    }
}
