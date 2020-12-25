using UnityEngine;

/// Manages the background GameObject.
public class Background : MonoBehaviour
{
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("MainCamera")[0];
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = target.transform.position;
    }
}
