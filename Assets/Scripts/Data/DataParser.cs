using UnityEngine;

using System.IO;
using System.Text;
using System.Security.Cryptography;


public static class DataParser
{
	public static string Encrypt(string s)
	{
		if (DataManager.Instance.encrypt) {
			byte[] inputbuffer = Encoding.Unicode.GetBytes (s);
			byte[] outputBuffer = DES.Create ().CreateEncryptor (Data.key, Data.iv).TransformFinalBlock (inputbuffer, 0, inputbuffer.Length);
			return System.Convert.ToBase64String (outputBuffer);
		} else return s;
	}

	public static ScriptableObject CreateAsset<T>(string name) where T : ScriptableObject
    {
		T _asset = ScriptableObject.CreateInstance<T> () as T;
			
		return _asset;
    }
	
		
    public static void SaveJSON(string name, string info)
    {
        var _path = Application.persistentDataPath + "/Resources/" + name + ".json";
		if (!File.Exists (_path)) File.Delete(_path);
		Debug.Log("Creating JSON file... " + _path);

        using (FileStream fs = new FileStream(_path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
				writer.Write(Encrypt(info));
            }
        }
    }
}

