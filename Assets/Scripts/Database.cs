using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kevin.Database
{
    [CreateAssetMenu]
    public class Database : ScriptableObject
    {
        [SerializeField, Header("Player's")]
        private List<PlayerInfo> _playerInfo = new List<PlayerInfo>();
        public Dictionary<string, PlayerInfo> playerInfo = new Dictionary<string, PlayerInfo>();

        [SerializeField, Header("Race's")]
        private List<Race> _races = new List<Race>();
        public Dictionary<string, Race> races = new Dictionary<string, Race>();

        [SerializeField, Header("Profession's")]
        private List<Profession> _professions = new List<Profession>();
        public Dictionary<string, Profession> professions = new Dictionary<string, Profession>();

        [SerializeField, Header("Armor set")]
        private Armor _armor;

        public void Init()
        {
            playerInfo.Clear();
            for (var i = 0; i < _playerInfo.Count; i++)
                playerInfo.Add(_playerInfo[i].name, _playerInfo[i]);

            races.Clear();
            for (var i = 0; i < _races.Count; i++)
                races.Add(_races[i].name, _races[i]);

            professions.Clear();
            for (var i = 0; i < _professions.Count; i++)
                professions.Add(_professions[i].name, _professions[i]);
        }

        public void AddPlayerInfo(string name, string race, string profession)
        {
            if (name != null)
            {
                var length = _playerInfo.Count;
                for (var i = 0; i < length; i++)
                    if (name == _playerInfo[i].name) {
                        Debug.Log("Sorry, this name is already taken.");
                        return;
                    } 

                var info = (PlayerInfo)Extensions.CreateAsset<PlayerInfo>(name, "Assets/Scripts/Players");

                _playerInfo.Add(info);

                var current = _playerInfo.Count - 1;

                _playerInfo[current].name = name;
                _playerInfo[current].race = races[race];
                _playerInfo[current].profession = professions[profession];
                _playerInfo[current].armor = _armor;

                Init();
            }
        }
    }
}

