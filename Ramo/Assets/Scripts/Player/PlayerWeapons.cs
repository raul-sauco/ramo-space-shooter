using UnityEngine;

/// <summary>
/// Manages the player character's weapons and attacks.
/// </summary>
public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform ShootingPoint;
    [SerializeField] private AudioSource shootLaser12Sfx;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Shoot();
        }

        // Enable mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        shootLaser12Sfx.Play();
        Instantiate(BulletPrefab, ShootingPoint.position, ShootingPoint.rotation);
    }
}
