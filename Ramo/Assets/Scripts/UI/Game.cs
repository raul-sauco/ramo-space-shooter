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
    [SerializeField] private GameObject levelCompleteOverlay;

    private bool isGameActive;
    // Keep a reference to unsubscribe.
    private Boss bossScript;

    #region lifecycle

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        SubscribeToBossDestroyedEvent();
    }

    // Update is called once per frame
    void Update()
    {
        // KeyCode.Escape also maps to Android back button.
        if (Input.GetKeyDown(KeyCode.Escape)) { TogglePause(); }
    }

    // Clean up before the object is disabled.
    void OnDisable()
    {
        if (bossScript != null)
            bossScript.OnDestroyed -= BossDestroyedCallback;
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

    // The default behaviour is to mark the level as completed when the boss
    // is destroyed.
    private void BossDestroyedCallback()
    {
        levelCompleteOverlay.SetActive(true);
        Invoke(nameof(NextLevel), 5f);
    }

    // Load the next scene in the build settings.
    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    #endregion // events
}
