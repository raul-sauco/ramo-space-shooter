using UnityEngine;

/// <summary>
/// Spinner rotates an object over time.
/// 
/// The script uses localRotation to allow nesting spinner objects.
/// </summary>
public class Spinner : MonoBehaviour
{
    [SerializeField] private Vector3 speed;

    // Start is called before the first frame update
    void Start()
    {
        if (speed == null)
        {
            speed = new Vector3(1f, 1f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime);
    }
}
