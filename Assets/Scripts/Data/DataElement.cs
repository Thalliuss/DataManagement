using UnityEngine;
using System.Collections;

public class DataElement : ScriptableObject
{
    [Header("Element's ID:"), SerializeField]
    private string _id;
    public string Id
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }
}
