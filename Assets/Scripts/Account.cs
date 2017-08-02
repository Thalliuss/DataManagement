using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu]
public class Account : ScriptableObject, IDatabaseElement
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

    [Header("Element's Username:")]
    public string username;

    [Header("Element's Password:")]
    public string password;
}
