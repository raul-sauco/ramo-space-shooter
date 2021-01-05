using UnityEngine;

/// <summary>
/// Control the end of game credit screen.
/// </summary>
public class Credits : MonoBehaviour
{
    [SerializeField] private float speed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
