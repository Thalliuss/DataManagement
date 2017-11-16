using DataManagement;

using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private SceneManager _sceneManager;
    private DataReferences _dataReferences;

    [SerializeField] private InputField _username;
    [SerializeField] private InputField _password;

    private void Start()
    {
        _sceneManager = SceneManager.Instance;
        _dataReferences = _sceneManager.DataReferences;
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
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
            }
        }
        else StartCoroutine(Error());
    }

    public void OnSignUpButtonClicked()
    {
        Account t_account = _dataReferences.FindElement<Account>(_username.text.ToUpper());
        if (t_account == null)
        {
            _dataReferences.AddElement<Account>(_username.text.ToUpper());
            t_account = _dataReferences.FindElement<Account>(_username.text.ToUpper());

            t_account.Username = _username.text;
            t_account.Password = _password.text;
            t_account.Save();
        }
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

