using System;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInfo : ScriptableObject
{
    [Header("PlayerName.")]
    public string name;

    [Header("Race.")]
    public Race race;

    [Header("Profession.")]
    public Profession profession;

    [Serializable]
    public class Statistics
    {
        public int health = 100;
        public int stamina = 100;
        public int mana = 100;
    }
    [Header("Statistic's.")]
    public Statistics statistics;

    [Header("Armor")]
    public Armor armor;

    [Header("Weapon")]
    public Weapon weapon;
}
