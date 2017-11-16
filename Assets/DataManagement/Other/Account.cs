using DataManagement;

using UnityEngine;

public class Account : DataElement
{

    public string Username
    {
        get
        {
            return _username;
        }

        set
        {
            _username = value;
        }
    }
    [SerializeField] private string _username;

    public string Password
    {
        get
        {
            return _password;
        }

        set
        {
            _password = value;
        }
    }
    [SerializeField] private string _password;

    public Account(string p_id) : base(p_id)
    {
        ID = p_id;
    }
}
