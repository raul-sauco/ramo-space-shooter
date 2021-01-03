using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Game manages all logic not directly related
/// to one particular GameObject.
/// </summary>
public class Game : MonoBehaviour
{
    // Singleton instance.
    public static Game Instance { get; private set; }

    // References
    // Set on inspector because it is non active at game start.
    [SerializeField] private GameObject pauseOverlay;

    private bool isGameActive;
    

   #region lifecycle

    // Called once when created.
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        // KeyCode.Escape also maps to Android back button.
        if (Input.GetKeyDown(KeyCode.Escape)) { TogglePause(); }
    }

    #endregion  // Lifecycle

    #region user-interaction

    public void TogglePause()
    {
        isGameActive = !isGameActive;
        pauseOverlay.SetActive(!isGameActive);
        Debug.Log("Game " + (isGameActive ? " restarted" : " paused"));
        if (isGameActive)
        {
            Time.timeScale = 1;
        } else
        {
            Time.timeScale = 0;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    #endregion // user-interaction
}
