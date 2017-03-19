using Kevin.Database;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreation : MonoBehaviour
{
    [SerializeField, Header("PlayerName.")]
    private string _name;
    [SerializeField] private Text _input;

    [SerializeField, Header("Race's, Classes and Weapons")]
    private Text _race;
    [SerializeField] private Text _profession;
    [SerializeField] private Text _weapon;

    [SerializeField, Header("Database.")]
    private Database _database;

    public void CreatePlayerInfo()
    {
        _database.Init();
        _database.AddPlayerInfo(_name, _race.text, _profession.text, _weapon.text);
    }

    private void Update()
    {
        _name = _input.text;
    }
}
