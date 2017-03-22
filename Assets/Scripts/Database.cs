using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Database : ScriptableObject
{
    [Header("Account's")]
    public List<string> usernames;
    public List<Account> accounts;

    [Header("Race's")]
    public List<Race> races;

    [Header("Profession's")]
    public List<Profession> professions;

    [Header("Armor set")]
    public Armor armor;

    [Header("Weapon's.")]
    public List<Weapon> weapons;
}

