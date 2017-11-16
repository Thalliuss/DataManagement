using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingscreenManager : MonoBehaviour
{
    private static LoadingscreenManager _instance;
    public static LoadingscreenManager Instance
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

    public bool IsLoading
    {
        get
        {
            return _isLoading;
        }

        set
        {
            _isLoading = value;
        }
    }
    private bool _isLoading = false;

    [SerializeField] private bool _loadOnStart;
    [SerializeField] private string _levelToLoad;

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);

        _instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (_loadOnStart) LoadScene(_levelToLoad);
    }

    public void LoadScene(string p_input)
    {
        SceneManager.LoadScene(p_input);
    }

    public void OpenLoadingscreen(float p_current, float p_max, string p_text)
    {
        UIManager t_uiManager = UIManager.Instance;

        t_uiManager.OpenLoading(p_text);
        _isLoading = true;

        t_uiManager.LoadingBar.value = p_current / p_max;
    }

    public void CloseLoadingscreen()
    {
        UIManager t_uiManager = UIManager.Instance;

        t_uiManager.CloseLoading();
        _isLoading = false;
    }
}

