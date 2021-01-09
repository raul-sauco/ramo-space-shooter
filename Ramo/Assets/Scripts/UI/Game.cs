using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game manages all logic not directly related
/// to one particular GameObject.
/// </summary>
public class Game : MonoBehaviour
{
    // Set on inspector because it is non active at game start.
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private GameObject gameOverOverlay;
    [SerializeField] private GameObject levelCompleteOverlay;
    [SerializeField] private GameObject loadingOverlay;
    [SerializeField] private GameObject loadNextOverlay;

    private bool isGameActive;
    private bool isGameOver;
    private bool isLevelCompleted;
    // Keep a reference to unsubscribe.
    private Boss bossScript;
    private Player playerScript;

    #region lifecycle

    // Start is called before the first frame update
    void Start()
    {
        isLevelCompleted = false;
        isGameActive = true;
        isGameOver = false;
        SubscribeToBossDestroyedEvent();
        SubscribeToPlayerDestroyedEvent();
    }

    // Update is called once per frame
    void Update()
    {
        // KeyCode.Escape also maps to Android back button.
        if (!isGameOver && Input.GetKeyDown(KeyCode.Escape)) 
        {
            TogglePause();
        }
        if (isLevelCompleted)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.touchCount > 0)
            {
                LoadNextLevel();
            }
        }
    }

    // Clean up before the object is destroyed.
    void OnDestroy()
    {
        if (bossScript != null)
        {
            bossScript.OnDestroyed -= BossDestroyedCallback;
        }
        if (playerScript != null)            
        {
            playerScript.OnDestroyed -= PlayerDestroyedCallback;
        }
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

    public void NewGame()
    {
        // Make sure we destroy the player to get a new one
        Time.timeScale = 1;
        Destroy(PlayerState.Instance.gameObject);
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    #endregion // user-interaction

    #region events

    // We want to know when the scene's boss is destroyed.
    // Each scene can only have one GameObject tagged "Boss"
    void SubscribeToBossDestroyedEvent()
    {
        GameObject bossGo = GameObject.FindWithTag("Boss");
        if (bossGo != null)
        {
            bossScript = bossGo.GetComponent<Boss>();
            if (bossScript != null)
            {
                bossScript.OnDestroyed += BossDestroyedCallback;
            }
        } else 
            Debug.LogWarning("Could not find scene's boss GameObject");
    }

    // If the player character is destroyed, the game is over.
    void SubscribeToPlayerDestroyedEvent()
    {
        GameObject player = PlayerState.Instance.gameObject;
        if (player != null)
        {
            playerScript = player.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.OnDestroyed += PlayerDestroyedCallback;
            }
        } else
        {
            Debug.LogWarning("Could not find scene's player GameObject");
        }
    }

    // The default behaviour is to mark the level as completed when the boss
    // is destroyed.
    private void BossDestroyedCallback()
    {
        levelCompleteOverlay.SetActive(true);
        loadNextOverlay.SetActive(true);
        isLevelCompleted = true;
    }

    // Default game behaviour when player character is destroyed.
    private void PlayerDestroyedCallback()
    {
        isGameOver = true;
        gameOverOverlay.SetActive(true);
    }

    // Load the next scene in the build settings.
    private void LoadNextLevel()
    {
        levelCompleteOverlay.SetActive(false);
        loadNextOverlay.SetActive(false);
        loadingOverlay.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    #endregion // events
}
