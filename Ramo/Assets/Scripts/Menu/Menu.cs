using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the main menu at the start of the game.
/// </summary>
public class Menu : MonoBehaviour
{
    [SerializeField] private Transform roamingLight;
    [SerializeField] private float range = 30f;
    [SerializeField] private float speed = 2f;

    void Update()
    {
        if (roamingLight.position.x < range)
            roamingLight.Translate(Vector3.right * speed * Time.deltaTime);
        else
            roamingLight.position = new Vector3(range * -1, 
                roamingLight.position.y, roamingLight.position.z);
    }

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
