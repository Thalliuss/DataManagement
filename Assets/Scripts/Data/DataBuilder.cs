using UnityEngine;

using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class DataBuilder
{
	public static string Decrypt(string s) 
	{
		if (DataManager.Instance.encrypt) {
			byte[] inputbuffer = System.Convert.FromBase64String (s);
			byte[] outputBuffer = DES.Create ().CreateDecryptor (DataReferences.key, DataReferences.iv).TransformFinalBlock (inputbuffer, 0, inputbuffer.Length);
			return Encoding.Unicode.GetString (outputBuffer);
		} else return s;
	}

    public static void BuildDataReferences()
	{
        var _path = Application.persistentDataPath + "/" + DataManager.Instance.DataReferences.ID + "/" + DataManager.Instance.DataReferences.ID  + ".json";

        if (File.Exists(_path)) {
            JsonUtility.FromJsonOverwrite(Decrypt(File.ReadAllText(_path)), DataManager.Instance.DataReferences);
        }
    }

	public static void BuildElement<T>(DataReferences.SavedElement saveData, int index) where T : DataElement
    {
		var _id = saveData.ids[index].ToString();
		var _path = Application.persistentDataPath + "/" + DataManager.Instance.DataReferences.ID + "/" + _id + ".json";

        if (File.Exists(_path)) {
			var _element = DataParser.CreateAsset<T> (_id) as T;
			JsonUtility.FromJsonOverwrite(Decrypt(File.ReadAllText(_path)), _element);

			saveData.info[index] = _element;
        }
    }
}
