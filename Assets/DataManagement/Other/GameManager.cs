using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
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

    public Account CurrentAccount
    {
        get
        {
            return _currentAccount;
        }

        set
        {
            _currentAccount = value;
        }
    }
    private Account _currentAccount;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (_instance != null)
            Destroy(gameObject);

        _instance = this;
    }
}
