
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
        GameObject go = GameObject.FindWithTag("FloatingJoystick");
        if (go != null)
        {
            joystick = go.GetComponent<Joystick>();
        } else 
        {
            Debug.LogWarning("Joystick is null and couldn't find Joystick game object in scene");
        } 
    }

    void Update()
    {
        // Get a reference to the Joystic on new scenes.
        if (joystick == null)
        {
            GameObject go = GameObject.FindWithTag("FloatingJoystick");
            if (go != null)
            {
                joystick = go.GetComponent<Joystick>();
            } else 
            {
                Debug.LogWarning("Joystick is null and couldn't find Joystick game object in scene");
            }
        }
    }

    public void FixedUpdate()
    {
        rb.AddForce(
            new Vector2(calculateForceX(), calculateForceY()) * speed * Time.fixedDeltaTime, 
            ForceMode.Force);
    }

    // Calculate Y force adding all entry methods.
    private float calculateForceY()
    {
        var push = Input.GetAxisRaw("Vertical");
        if (joystick != null)
        {
            push += joystick.Vertical;
        }
        if (push > 0 && rb.velocity.y < maxVelocity)
            return push;
        if (push < 0 && rb.velocity.y > (-1 * maxVelocity))
            return push;
        return 0;
    }

    // Calculate X force adding all entry methods.
    private float calculateForceX()
    {
        var push = Input.GetAxisRaw("Horizontal");
        if (joystick != null)
        {
            push += joystick.Horizontal;
        }
        if (push > 0 && rb.velocity.x < maxVelocity)
            return push;
        if (push < 0 && rb.velocity.x > (-1 * maxVelocity))
            return push;
        return 0;
    }
}
