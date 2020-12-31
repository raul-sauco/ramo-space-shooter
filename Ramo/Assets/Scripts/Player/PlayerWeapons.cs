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
    }

    private void Shoot()
    {
        Instantiate(BulletPrefab, ShootingPoint.position, ShootingPoint.rotation);
    }
}
