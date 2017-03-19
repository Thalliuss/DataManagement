using Kevin.Database;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreation : MonoBehaviour
{
    [SerializeField, Header("PlayerName.")]
    private string _name;
    [SerializeField] private Text _input;

    [SerializeField, Header("Race's, and Classes")]
    private Text _race;
    [SerializeField] private Text _profession;

    [SerializeField, Header("Database.")]
    private Database _database;

    public void Start()
    {
        _database.Init();
    }

    public void CreatePlayerInfo()
    {
        _database.Init();
        _database.AddPlayerInfo(_name, _race.text, _profession.text);
    }

    private void Update()
    {
        _name = _input.text;
    }
}
