using DataManagement;

using UnityEngine;

public class Character : DataElement
{
    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
        }
    }
    [SerializeField] private string _name;

    public Character(string id, string name) : base(id)
    {
        ID = id;
        _name = name;
    }

    public void Select()
    {
        var _characterManager = CharacterManager.Instance;

        _characterManager.current = this;
    }
}