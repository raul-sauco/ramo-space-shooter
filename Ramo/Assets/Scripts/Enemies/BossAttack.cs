using System.Collections; 
using UnityEngine;

/// <summary>
/// Manages the attacks that a boss enemy makes.
/// </summary>
public class BossAttack : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform cannon;
    [SerializeField] private float minWait;
    [SerializeField] private float maxWait;
    [SerializeField] private float fromActiveWait;
    [SerializeField] private int shotsFired = 3;
    [SerializeField] private float bulletSpeed = 50f;
    [SerializeField] private float bossRotationOnWorldSpace = 90;
    [SerializeField] private bool aimCentralShotToPlayer = false;
    [SerializeField] private AudioSource shootSfx;

    // Flag wether the gameObject is active.
    private bool isActive;
    // Keep a reference to the Boss script.
    private Boss boss;
    private Transform target;

    void Start()
    {
        isActive = false;
        boss = GetComponent<Boss>();
        if (boss != null)
        {
            boss.OnActivated += OnActivatedCallback;
            boss.OnDestroyed += OnDestroyedCallback;
        } else 
        {
            Debug.LogWarning("Could not find a reference to Boss script");
        }

        GameObject go = PlayerState.Instance.gameObject;
        if (go != null)
        {
            target = go.transform;
        } else
        {
            Debug.LogWarning("Boss GameObject could not find player");
        }
    }
    
    // Clean up before the object is disabled.
    void OnDisable()
    {
        if (boss != null)
        {
            boss.OnActivated -= OnActivatedCallback;
            boss.OnDestroyed -= OnDestroyedCallback;
        }
    }

    void OnActivatedCallback()
    {
        isActive = true;
        StartCoroutine(nameof(Attack));
        Debug.Log(gameObject.name + " attacking");
    }

    void OnDestroyedCallback()
    {
        isActive = false;
    }

    private void Shoot()
    {
        shootSfx.Play();
        // Diagonally up.
        GameObject bulletInstance = Instantiate(bulletPrefab, cannon.position, cannon.rotation);
        AssignDirection(-45, bulletInstance);
        // Straight.
        bulletInstance = Instantiate(bulletPrefab, cannon.position, cannon.rotation);
        AssignDirection(0, bulletInstance);
        // Diagonally down.
        bulletInstance = Instantiate(bulletPrefab, cannon.position, cannon.rotation);
        AssignDirection(45, bulletInstance);

        if (shotsFired > 3 && shotsFired < 6)
        {
            bulletInstance = Instantiate(bulletPrefab, cannon.position, cannon.rotation);
            AssignDirection(-22, bulletInstance);
            bulletInstance = Instantiate(bulletPrefab, cannon.position, cannon.rotation);
            AssignDirection(22, bulletInstance);
        } else if (shotsFired >= 6)
        {
            bulletInstance = Instantiate(bulletPrefab, cannon.position, cannon.rotation);
            AssignDirection(-15, bulletInstance);
            bulletInstance = Instantiate(bulletPrefab, cannon.position, cannon.rotation);
            AssignDirection(15, bulletInstance);
            bulletInstance = Instantiate(bulletPrefab, cannon.position, cannon.rotation);
            AssignDirection(-30, bulletInstance);
            bulletInstance = Instantiate(bulletPrefab, cannon.position, cannon.rotation);
            AssignDirection(30, bulletInstance);
        }
    }

    // Get a handle to the instantiated object and set its direction.
    private void AssignDirection(float angle, GameObject bulletGO)
    {
        Vector3 direction;
        if (angle == 0f && aimCentralShotToPlayer)
        {
            direction = (target.transform.position - transform.position).normalized;
        } else
        {
            // Boss is rotated on scene
            var bossRot = Quaternion.AngleAxis(bossRotationOnWorldSpace, Vector3.right);
            var rotation = Quaternion.AngleAxis(angle, Vector3.up);
            direction = bossRot * rotation * Vector3.left;
        }
        Bullet controller =  bulletGO.GetComponent<Bullet>();
        if (controller != null)
        {
            controller.SetSpeed(direction * bulletSpeed);
        } else
        {
            Debug.LogWarning("No bullet controller found");
        }
            
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(fromActiveWait);

        while(isActive)
        {
            Shoot();
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
        }
    }
}
