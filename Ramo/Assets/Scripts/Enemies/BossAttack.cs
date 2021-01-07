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
    [SerializeField] private AudioSource shootSfx;

    // Flag wether the gameObject is active.
    private bool isActive;
    // Keep a reference to the Boss script.
    private Boss boss;

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
        AssignDirection(new Vector3(-26.26f, 26.26f, 0f), bulletInstance);
        // Straight.
        bulletInstance = Instantiate(bulletPrefab, cannon.position, cannon.rotation);
        AssignDirection(new Vector3(-50f, 0f, 0f), bulletInstance);
        // Diagonally down.
        bulletInstance = Instantiate(bulletPrefab, cannon.position, cannon.rotation);
        AssignDirection(new Vector3(-26.26f, -26.26f, 0f), bulletInstance);
    }

    // Get a handle to the instantiated object and set its direction.
    private void AssignDirection(Vector3 speed, GameObject bulletGO)
    {
        Bullet controller =  bulletGO.GetComponent<Bullet>();
        if (controller != null)
            controller.SetSpeed(speed);
        else
            Debug.LogWarning("Not bullet controller found");
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
