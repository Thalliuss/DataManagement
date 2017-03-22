using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField, Header("Account, Username and Password.")]
    private Account _account;
    [SerializeField]
    private InputField _username;
    [SerializeField]
    private InputField _password;

    [SerializeField, Header("Login and Create button.")]
    private Button _login;
    [SerializeField]
    private Button _create;

    [SerializeField, Header("Scene to load.")]
    private string _sceneToLoad;

    private Database _database;

    private void Start()
    {
        _database = GameManager.Instance.database;

        _login.onClick.AddListener(() =>
        {
            if (_account == null)
            {
                _account = DatabaseHelper.Login(_database, _username.text, _password.text);
                if (_account == null) {
                    StartCoroutine(AccountNotFound());
                    return;
                }
                SceneManager.LoadScene(_sceneToLoad);
            }
        });
    }

    private IEnumerator AccountNotFound()
    {
        _username.text = "";
        _password.text = "";

        _username.interactable = false;
        _password.interactable = false;
        _login.interactable = false;

        yield return new WaitForSeconds(2f);

        _username.interactable = true;
        _password.interactable = true;
        _login.interactable = true;
    }
}
