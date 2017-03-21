using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Database : ScriptableObject
{
    [Header("Player's.")]
    public List<string> playerNames = new List<string>();
    public List<PlayerInfo> playerInfo = new List<PlayerInfo>();

    [Header("Race's")]
    public List<Race> races = new List<Race>();

    [Header("Profession's")]
    public List<Profession> professions = new List<Profession>();

    [Header("Armor set")]
    public Armor armor;

    [Header("Weapon's.")]
    public List<Weapon> weapons = new List<Weapon>();
}

