using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Control the end of game credit screen.
/// </summary>
public class Credits : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private GameObject exitOverlay;

    // Update is called once per frame
    void Update()
    {
        // Move the slider.
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Listen for user interaction        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (exitOverlay.activeSelf)
            {
                HideExitMenu();
            } else
            {
                ShowExitMenu();
            }
        }
    }

    private void ShowExitMenu()
    {
        exitOverlay.SetActive(true);
        Time.timeScale = 0;
    }

    public void HideExitMenu()
    {
        exitOverlay.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        // Destroy the player prefab, could be null during development.
        if (PlayerState.Instance != null && PlayerState.Instance.gameObject != null)
        {
            Destroy(PlayerState.Instance.gameObject);
        }
        // Reset the timeScale that the menu stopped.
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
