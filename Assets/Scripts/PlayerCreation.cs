using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreation : MonoBehaviour
{
    [SerializeField, Header("PlayerName.")]
    private string _name;
    [SerializeField] private InputField _input;


    [SerializeField, Header("Race's, Classes and Weapons")]
    private Dropdown _race;
    [SerializeField] private Dropdown _profession;
    [SerializeField] private Dropdown _weapon;

    [SerializeField] private Transform _players;
    [SerializeField] private GameObject _prefab;

    private int _id;
    private List<Player> _instPlayers = new List<Player>();


    public void Start()
    {
        var database = Main.Instance.database;
        for (var i = 0; i < database.playerInfo.Count; i++) {
            if (File.Exists(Application.dataPath + "/Resources/" + database.playerNames[i] + ".json")) {
                database.playerInfo[i] = new PlayerInfo();
                JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/Resources/" + database.playerNames[i] + ".json"), database.playerInfo[i]);
            }
            _instPlayers.Add(Instantiate(_prefab, _players, false).GetComponent<Player>());
            _instPlayers[i].name = database.playerInfo[i].name;

            _instPlayers[i].LoadAssets(i);
        }
    }

    public void CreatePlayerInfo()
    {
        var text = "Enter text...";
        _input.placeholder.GetComponent<Text>().text = text;

        if (!File.Exists(Application.dataPath + "/Resources/" + _name + ".json") || !File.Exists("Assets/Scripts/Players/" + _name + ".asset"))
        {
            var database = Main.Instance.database;
            text = DatabaseHelper.AddPlayerInfo(database, _name, _race.value, _profession.value, _weapon.value);
            Debug.Log(text);

            if (text == _name)
            {
                var player = Instantiate(_prefab, _players, false).GetComponent<Player>();
                player.name = _name;

                player.LoadAssets(_id);
                _id++;
                return;
            }
        }
        _input.text = "";
        StartCoroutine(ErrorMessage(text));
    }

    private IEnumerator ErrorMessage(string text)
    {
        _input.placeholder.GetComponent<Text>().text = text;
        _input.interactable = false;

        yield return new WaitForSeconds(2f);

        text = "Enter text...";
        _input.placeholder.GetComponent<Text>().text = text;
        _input.interactable = true;
    }

    private void Update()
    {
        _name = _input.text;
    }
}
