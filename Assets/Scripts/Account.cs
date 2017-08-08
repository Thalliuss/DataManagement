using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu]
public class Account : DataElement
{
    [Header("Element's Username:")]
    public string username;

    [Header("Element's Password:")]
    public string password;
}
