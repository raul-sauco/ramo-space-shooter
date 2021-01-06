
using UnityEngine;

/// <summary>
/// Circular movement gives an object a circular movement along the x and y 
/// axis according to the selected parameters.
/// </summary>
public class CircularMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rangeX = 30f;
    [SerializeField] private float rangeY = 20f;
    private float control;

    void Start()
    {
        control = 0f;
    }

    void Update()
    {
        control += Time.deltaTime * speed;
        float x = Mathf.Cos(control) * rangeX;
        float y = Mathf.Sin(control) * rangeY;
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
