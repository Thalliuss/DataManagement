using UnityEngine;
using DataManagement;
using UnityEngine.UI;

public class Example : MonoBehaviour {

	#region Save implementation
	// A reference too all currently saved data.
	private DataReferences _dataReferences = null;

	// A reference too the data being saved in this class.
	private ExampleData _data = null;

	// The ID under wich this data will be saved.
	private const string _id = "Example";

	private void Setup()
	{
		_dataReferences = SceneManager.Instance.DataReferences;

		_data = _dataReferences.FindElement<ExampleData>(_id);
		if (_data == null) _data = _dataReferences.AddElement<ExampleData>(_id);

		LoadText();
	}

	public void SaveText()
	{
		_data.Text = _input.text;
		_data.Save();
	}

	public void LoadText()
	{
		_input.text = _data.Text;
	}

	#endregion

	[SerializeField] private InputField _input = null;

	void Start()
	{
		Setup();
	}
}
