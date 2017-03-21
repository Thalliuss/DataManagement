using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreation : MonoBehaviour
{
    [SerializeField, Header("Player name.")]
    private string _name;
    [SerializeField] private InputField _input;

    [SerializeField, Header("Race's, Classes and Weapons")]
    private Dropdown _race;
    [SerializeField] private Dropdown _profession;
    [SerializeField] private Dropdown _weapon;

    [SerializeField, Header("Player presets.")]
    private Transform _players;
    [SerializeField] private GameObject _prefab;

    [SerializeField, Header("Creation button.")]
    private Button _create;

    private int _id;
    private List<Player> _instPlayers = new List<Player>();

    private Database _database;

    public void Start()
    {
        _database = GameManager.Instance.database;
        for (var i = 0; i < _database.playerInfo.Count; i++) {
            _instPlayers.Add(Instantiate(_prefab, _players, false).GetComponent<Player>());
            _instPlayers[i].name = _database.playerInfo[i].name;
        }
    }

    public void CreatePlayerInfo()
    {
        var text = DatabaseHelper.AddPlayerInfo(_database, _name, _race.value, _profession.value, _weapon.value);
        if (text == _name)
        {
            var player = Instantiate(_prefab, _players, false).GetComponent<Player>();
            player.name = _name;

            return;
        }
        _input.text = "";
        StartCoroutine(OnPlayerExist(text));
    }

    private IEnumerator OnPlayerExist(string text)
    {
        _input.placeholder.GetComponent<Text>().text = text;
        _input.interactable = false;
        _create.interactable = false;

        yield return new WaitForSeconds(2f);

        text = "Enter text...";
        _input.placeholder.GetComponent<Text>().text = text;
        _input.interactable = true;
        _create.interactable = true;
    }

    private void Update()
    {
        _name = _input.text;
    }
}
