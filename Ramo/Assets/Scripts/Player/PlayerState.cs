using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// PlayerState is a singleton that stores information about the current
/// player state across scenes.
/// </summary>
public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; private set; }

    #region lifecycle

    // This controls the player state. Singleton pattern.
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else { Destroy(gameObject); }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("PlayerState::OnSceneLoaded: " + scene.name);
        if (scene.name == "Credits")
        {
            Debug.Log("Loaded credit scene, destroying player");
            Destroy(gameObject);
        } else 
        {
            // Reset the player to the start position, always 0,0,0.
            transform.position = new Vector3(0f,0f,0f);
        }
    }

    // called on object disable.
    void OnDestroy()
    {
        Debug.Log("PlayerState::OnDestroy");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #endregion  // lifecycle
}
