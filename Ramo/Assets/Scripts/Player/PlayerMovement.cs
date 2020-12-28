
using UnityEngine;

/// <summary>
/// Controls player movemement.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed = 300f;
    [SerializeField] private float maxVelocity = 10.0f;
    
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    public void FixedUpdate()
    {
        rb.AddForce(
            new Vector2(calculateForceX(), calculateForceY()) * speed * Time.fixedDeltaTime, 
            ForceMode.Force);
    }

    private float calculateForceY()
    {
        var push = joystick.Vertical + Input.GetAxisRaw("Vertical");
        if (push > 0 && rb.velocity.y < maxVelocity)
            return push;
        if (push < 0 && rb.velocity.y > (-1 * maxVelocity))
            return push;
        return 0;
    }

    private float calculateForceX()
    {
        var push = joystick.Horizontal + Input.GetAxisRaw("Horizontal");
        if (push > 0 && rb.velocity.x < maxVelocity)
            return push;
        if (push < 0 && rb.velocity.x > (-1 * maxVelocity))
            return push;
        return 0;
    }
}
