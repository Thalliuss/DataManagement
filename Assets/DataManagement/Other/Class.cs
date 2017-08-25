using DataManagement;

using UnityEngine;

[CreateAssetMenu]
public class Class : DataElement
{
    public Class(string id) : base(id)
    {
        ID = id;
    }

    public Sprite Icon
    {
        get
        {
            return _icon;
        }
        set
        {
            _icon = value;
        }
    }
    [SerializeField] private Sprite _icon;
}
