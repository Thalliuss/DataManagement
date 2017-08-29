using DataManagement;

using UnityEngine;

[CreateAssetMenu]
public class Class : DataElement
{
    public Class(string p_id) : base(p_id)
    {
        ID = p_id;
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
