using System.Collections;

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
        var _dataReferences = _dataManager.DataReferences;

        if (_dataReferences.FindElement<Account>(_username.text.ToUpper()) != null) {
            var _account = _dataReferences.FindElement<Account>(_username.text.ToUpper());
            if (_username.text == _account.Username  && _password.text == _account.Password) {
                SceneManager.LoadScene("Main");
            }
        } else StartCoroutine(Error());
    }

    public void OnSignUpButtonClicked()
    {
        var _dataReferences = _dataManager.DataReferences;

        if (_dataReferences.FindElement<Account>(_username.text.ToUpper()) == null) {
            _dataReferences.AddElement<Account>(new Account(_username.text.ToUpper(), _username.text, _password.text));
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

