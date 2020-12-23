using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls player movemement.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed;
    
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    public void FixedUpdate()
    {
        Vector2 force = Vector2.up * joystick.Vertical + 
            Vector2.right * joystick.Horizontal;
        rb.AddForce(force * speed * Time.fixedDeltaTime, 
            ForceMode2D.Force);
    }
}
