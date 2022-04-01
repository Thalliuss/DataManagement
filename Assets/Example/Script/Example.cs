using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DataManagement;
using System.Linq;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class Example : MonoBehaviour {

	#region Save implementation
	// A reference too all currently saved data.
	private DataReferences _dataReferences = null;

	// A reference too the data being saved in this class.
	private ExampleData _data = null;

	// The ID under wich this data will be saved.
	[SerializeField] private string _id = "none";

	[ContextMenu("Generate ID")]
	public void GenerateID()
	{
		if (!Application.isPlaying)
		#if UNITY_EDITOR
			EditorSceneManager.MarkSceneDirty(gameObject.scene);
		#endif

		_id = "";
		System.Random t_random = new System.Random();
		const string t_chars = "AaBbCcDdErFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
		_id = new string( Enumerable.Repeat(t_chars, 8).Select(s => s[t_random.Next(s.Length)]).ToArray());
	}

	public bool Setup(string p_id)
	{
		if (_id == "none") return false;

		_id = p_id;

		StartCoroutine(ContinuousSave());

		_dataReferences = SceneManager.Instance.DataReferences;

		_data = _dataReferences.FindElement<ExampleData>(_id);
		if (_data == null) {
			_data = _dataReferences.AddElement<ExampleData>(_id);
			return false;
		}

		LoadText();

		return true;
	}
	public void SaveText()
	{
		_data.Text = _text;
		_data.Save();
	}
	public void LoadText()
	{
		_text = _data.Text;
	}

	private IEnumerator ContinuousSave()
	{
		while (true)
		{
			yield return new WaitForSeconds(.5f);

		}
	}

	#endregion

	private string _text;

	[SerializeField] private InputField _input;

	private void Start()
	{
		if(!Setup(_id))
			return;

		_input.text = _text;
	}

    public void SetText()
    {
		_text = _input.text;
	}
}