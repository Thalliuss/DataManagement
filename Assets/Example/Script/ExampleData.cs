using UnityEngine;
using DataManagement;

public class ExampleData : DataElement {

	public string Text { get => _text; set => _text = value; }
	[SerializeField] private string _text;

	public ExampleData(string p_id) : base(p_id)
	{
		ID = p_id;
	}
}
