using DataManagement;

using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            return _instance;
        }

        set
        {
            _instance = value;
        }
    }

    private DataManager _dataManager;

    [Header("Characters."), SerializeField]
    private List<Character> _characters = new List<Character>();
    private int _characterID;

    [Header("UI Elements - Characters.")]
    [SerializeField]
    private GameObject _characterField;
    [SerializeField] private Image _icon;
    [SerializeField] private Text _title;

    [Header("UI Elements - Create.")]
    [SerializeField]
    private GameObject _createField;
    [SerializeField] private InputField _inputName;
    [SerializeField] private Dropdown _classSelect;

    [Header("Prefab.")]
    public Character current;

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);

        _instance = this;
    }

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        _dataManager = DataManager.Instance;
        var _gameManager = GameManager.Instance;

        if (_gameManager.CurrentAccount.SaveData.ids.Count == 0) return;

        _characters.Clear();

        for (int i = 0; i < _gameManager.CurrentAccount.SaveData.ids.Count; i++)
            _characters.Add(_gameManager.CurrentAccount.FindElement<Character>(i));

        current = _characters[0];
        _title.text = current.Name;
        _icon.sprite = current.FindElementsOfType<Class>()[0].Icon;

        if (current == null)
            _characterField.SetActive(false);

        else _characterField.SetActive(true);

    }

    public Character GetCharacter(int id)
    {
        if (id <= _characters.Count)
            return _characters[id];

        return null;
    }

    public void Next()
    {
        _characterID++;
        if (_characterID > _characters.Count - 1)
            _characterID = 0;

        _icon.sprite = _characters[_characterID].FindElementsOfType<Class>()[0].Icon;
        _title.text = _characters[_characterID].Name;
    }

    public void Previous()
    {
        _characterID--;
        if (_characterID < 0)
            _characterID = _characters.Count - 1;

        _icon.sprite = _characters[_characterID].FindElementsOfType<Class>()[0].Icon;
        _title.text = _characters[_characterID].Name;
    }

    public void Select()
    {
        _characters[_characterID].Select();
    }

    public void Create()
    {
        var _gameManager = GameManager.Instance;
        _characterField.SetActive(false);
        _createField.SetActive(true);

        for (int i = 0; i < _dataManager.DataReferences.SaveData.ids.Count; i++)
        {
            if (_inputName.text != "" && _dataManager.DataReferences.FindElement<Character>(_inputName.text.ToUpper()) == null)
            {
                _dataManager.DataReferences.FindElement<Account>(_gameManager.CurrentAccount.ID).AddElement<Character>(new Character(_inputName.text.ToUpper(), _inputName.text));

                var _character = _dataManager.DataReferences.FindElement<Account>(_gameManager.CurrentAccount.ID).FindElement<Character>(_inputName.text.ToUpper());
                _character.AddElement<Class>(_dataManager.DataReferences.FindDataElement<Class>(_classSelect.value));

                _characterField.SetActive(true);
                _createField.SetActive(false);

                Init();

                _inputName.text = "";
            }
            else return;
        }
    }
}
