using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private DataManager _dataManager;

    [SerializeField] private InputField _username;
    [SerializeField] private InputField _password;

    private void Start()
    {
        _dataManager = DataManager.Instance;
    }

    public void OnLoginButtonClicked ()
    {
        var _data = _dataManager.data;

        if (_data.FindElement(_username.text) != null) {
            var _account = (Account)_data.FindElement(_username.text);
            if (_account.username == _username.text && _account.password == _password.text) 
                SceneManager.LoadScene("Main");
        } else StartCoroutine(Error());
    }

    public void OnSignUpButtonClicked()
    {
        var _data = _dataManager.data;

        if (_data.FindElement(_username.text) == null) {
            var _account = new Account();

            _account.Id = _username.text;
            _data.saveData.ids.Add(_account.Id);

            _account.password = _password.text;
            _account.username = _username.text;

            _data.AddElement<Account>(_account);

        } else StartCoroutine(Error());
    }

    private IEnumerator Error()
    {
        _username.text = "";
        _password.text = "";

        _username.interactable = false;
        _password.interactable = false;

        yield return new WaitForSeconds(2f);

        _username.interactable = true;
        _password.interactable = true;
    }
}

