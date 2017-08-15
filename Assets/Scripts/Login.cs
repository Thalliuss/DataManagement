using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Collections;

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

        if (_data.FindElement<Account>(_username.text.ToUpper()) != null) {
            var _account = _data.FindElement<Account>(_username.text.ToUpper());
            if (_username.text == _account.username  && _password.text == _account.password) {
                SceneManager.LoadScene("Main");
            }
        } else StartCoroutine(Error());
    }

    public void OnSignUpButtonClicked()
    {
        var _data = _dataManager.data;

        if (_data.FindElement<Account>(_username.text) == null) {
            var _account = new Account();

            _account.ID = _username.text.ToUpper();

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

