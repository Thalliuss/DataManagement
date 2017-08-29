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

    public Character(string p_id, string p_name) : base(p_id)
    {
        ID = p_id;
        _name = p_name;
    }

    public void Select()
    {
        var t_characterManager = CharacterManager.Instance;

        t_characterManager.current = this;
    }
}