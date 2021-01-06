using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the main menu at the start of the game.
/// </summary>
public class Title : MonoBehaviour
{
    [SerializeField] private Transform roamingLight;
    [SerializeField] private float range = 30f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject loadingOverlay;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Play();
        }      
    }

    public void Play()
    {
        loadingOverlay.SetActive(true);
        SceneManager.LoadScene("Menu");
    }
}
