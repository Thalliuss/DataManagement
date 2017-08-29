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
    }
    [SerializeField] private string _username;

    public string Password
    {
        get
        {
            return _password;
        }
    }
    [SerializeField] private string _password;

    public Account(string p_id, string p_username, string p_password) : base(p_id)
    {
        ID = p_id;
        _username = p_username;
        _password = p_password;
    }
}
