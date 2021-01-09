using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the main menu at the start of the game.
/// </summary>
public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectOverlay;
    [SerializeField] private GameObject menuOverlay;
    [SerializeField] private GameObject loadingOverlay;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Default action is Play
            Play();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && levelSelectOverlay.activeSelf)
        {
            levelSelectOverlay.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ToggleLevelSelect()
    {
        levelSelectOverlay.SetActive(!levelSelectOverlay.activeSelf);
    }

    public void Play()
    {
        DisplayLoading();
        SceneManager.LoadScene("Level1");
    }

    public void PlayLevel1()
    {
        DisplayLoading();
        SceneManager.LoadScene("Level1");
    }

    public void PlayLevel2()
    {
        DisplayLoading();
        SceneManager.LoadScene("Level2");
    }

    public void PlayLevel3()
    {
        DisplayLoading();
        SceneManager.LoadScene("Level3");
    }

    public void PlayLevel4()
    {
        DisplayLoading();
        SceneManager.LoadScene("Level4");
    }

    public void PlayLevel5()
    {
        DisplayLoading();
        SceneManager.LoadScene("Level5");
    }

    public void PlaySurvival()
    {
        DisplayLoading();
        SceneManager.LoadScene("Survival");
    }

    // Prepare to load a new scene.
    private void DisplayLoading()
    {
        levelSelectOverlay.SetActive(false);
        menuOverlay.SetActive(false);
        loadingOverlay.SetActive(true);
    }
}
