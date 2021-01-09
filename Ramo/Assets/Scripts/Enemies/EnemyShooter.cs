
using System.Collections;
using UnityEngine;

/// <summary>
/// Manages non-boss enemy shooting.
/// </summary>
public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float minWait;
    [SerializeField] private float maxWait;
    [SerializeField] private float range;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private AudioSource shootSfx;

    private bool attacking;
    
    void Start()
    {
        attacking = false;
        GameObject playerGo = PlayerState.Instance.gameObject;
        if (playerGo != null)
        {
            target = playerGo.transform;
        } else
        {
            Debug.LogWarning("Could not find player object");
            Destroy(gameObject);
        }    
    }

    void Update()
    {
        if (!attacking)
        {
            // Avoid calculating distance if already attacking.
            var dist = Vector3.Distance(target.position, transform.position);
            if (dist < range)
            {
                attacking = true;
                StartCoroutine(nameof(Attack));
            }
        }
    }

    // Shoot towards the target
    private IEnumerator Attack()
    {
        while (target != null)
        {
            shootSfx.Play();
            GameObject bullet = Instantiate(bulletPrefab, 
                transform.position, transform.rotation);
            Bullet controller = bullet.GetComponent<Bullet>();
            Vector3 dir = target.position - transform.position;
            if (controller != null)
            {
                controller.SetSpeed(dir * bulletSpeed);
            } else
            {
                Debug.LogWarning("No bullet controller found");
            }
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
        }
    }
}
