using DataManagement;

using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private DataManager _dataManager;
    private DataReferences _dataReferences;

    [SerializeField] private InputField _username;
    [SerializeField] private InputField _password;

    private void Start()
    {
        _dataManager = DataManager.Instance;
        _dataReferences = _dataManager.DataReferences;
    }

    public void OnLoginButtonClicked()
    {
        var t_gameManager = GameManager.Instance;

        if (_dataReferences.FindElement<Account>(_username.text.ToUpper()) != null)
        {
            var t_account = _dataReferences.FindElement<Account>(_username.text.ToUpper());
            if (_username.text == t_account.Username && _password.text == t_account.Password)
            {
                t_gameManager.CurrentAccount = t_account;
                SceneManager.LoadScene("Main");
            }
        }
        else StartCoroutine(Error());
    }

    public void OnSignUpButtonClicked()
    {
        if (_dataReferences.FindElement<Account>(_username.text.ToUpper()) == null)
            _dataReferences.AddElement<Account>(new Account(_username.text.ToUpper(), _username.text, _password.text));
        
        else StartCoroutine(Error());
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

