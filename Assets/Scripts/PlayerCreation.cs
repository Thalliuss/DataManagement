using UnityEngine;
using UnityEngine.UI;

public class PlayerCreation : MonoBehaviour
{
    [SerializeField, Header("PlayerName.")]
    private string _name;
    [SerializeField] private Text _input;

    [SerializeField, Header("Race's, Classes and Weapons")]
    private Dropdown _race;
    [SerializeField] private Dropdown _profession;
    [SerializeField] private Dropdown _weapon;

    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _players;


    public void Start()
    {
        /*for (var i = 0; i < Main.Instance.database.playerInfo.Count; i++)
            Instantiate(_prefab, _players, false).GetComponent<Player>().name = Main.Instance.database.playerInfo[i].name;*/
    }

    public void CreatePlayerInfo()
    {
        var database = Main.Instance.database;
        //Instantiate(_prefab, _players, false).GetComponent<Player>().name = _name;

        database.AddPlayerInfo(_name, _race.value, _profession.value, _weapon.value);
    }

    private void Update()
    {
        _name = _input.text;
    }
}
