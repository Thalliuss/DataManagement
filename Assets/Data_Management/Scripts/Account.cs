using UnityEngine;

[CreateAssetMenu]
public class Account : DataElement
{
    public string Username
    {
        get {
            return _username;
        }
    }
    [SerializeField] private string _username;

    public string Password
    {
        get {
            return _password;
        }
    }
    [SerializeField] private string _password;

    public Account(string id, string username, string password) : base(id)
    {
        ID = id;
        _username = username;
        _password = password;
    }
}
