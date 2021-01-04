using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform ShootingPoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Shoot();
        }

        // Enable mobile
        if (Input.touchCount > 0)
        {
            // Do something
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2)
            {
                Debug.Log("Shooting by touch");
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Instantiate(BulletPrefab, ShootingPoint.position, ShootingPoint.rotation);
    }
}
