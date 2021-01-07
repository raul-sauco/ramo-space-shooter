using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages a boss type enemy.
/// </summary>
public class Boss : MonoBehaviour
{
    public delegate void Destroyed();
    public event Destroyed OnDestroyed;
    public delegate void Activated();
    public event Destroyed OnActivated;

    [SerializeField] private GameObject explossionPrefab;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private float offsetX = 20f;
    // The Y offset affects shooting accuracy of the Boss.
    [SerializeField] private float offsetY = 1.50f;
    [SerializeField] private float smoothSpeed = 0.1f;
    [SerializeField] private float activationDistance = 40f;
    [SerializeField] private float health = 100f;
    [SerializeField] private float hitDamage = 5f;

    private Transform playerTransform;
    private bool isActive;
    private GameObject explossionFx;
    private float startHealth;

    // Add two flags to optimize health bar color changes.
    private bool isOrange = false;
    private bool isRed = false;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        startHealth = health;
        GameObject go = PlayerState.Instance.gameObject;
        if (go != null)
            playerTransform = go.transform;
        else
            Debug.LogWarning("Boss GameObject could not find player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            float dist = Vector3.Distance(transform.position, playerTransform.position);
            if (dist < activationDistance)
            {
                // Activate the gameObject and notify subscribers.
                isActive = true;
                if (OnActivated != null)
                {
                    OnActivated();
                }
                Debug.Log("Activating " + gameObject.name);
            }
        }
    }

    void FixedUpdate()
    {
        if (isActive)
            TrackPlayer();
    }

    private void TrackPlayer()
    {
        Vector3 desiredPosition = playerTransform.position + 
            new Vector3(offsetX, offsetY, 0f);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (isActive && collider.gameObject.tag == "PlayerAttack")
            TakeDamage();
    }

    private void TakeDamage()
    {
        health -= hitDamage;
        float remainingHealth = health / startHealth;
        ColorizeHealthBar(remainingHealth);
        healthBar.transform.localScale = new Vector3(remainingHealth, 1f, 1f);
        if (health < 0)
        {
            // Notify observers before self-destroying.
            if (OnDestroyed != null)
            {
                OnDestroyed();
            }
            DestroySelf();
        }
    }

    private void ColorizeHealthBar(float remainingHealth)
    {
        // Do nothing while above first threshold
        if (remainingHealth < 0.5 && remainingHealth > 0.2 && !isOrange)
        {
            healthBar.GetComponent<Image>().color = new Color32(255, 140, 0, 255);
            isOrange = true;
        } else if (remainingHealth < 0.2 && !isRed)
        {
            healthBar.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            isRed = true;
        }
    }

    private void DestroySelf()
    {
        Vector3 position = new Vector3(transform.position.x, 
            transform.position.y, transform.position.z - 4);
        transform.localScale = new Vector3(0,0,0);
        explossionFx = Instantiate(explossionPrefab, position, transform.rotation);
        Invoke(nameof(CleanUpObjects), 2);
    }

    private void CleanUpObjects()
    {
        Destroy(explossionFx);
        Destroy(gameObject);
        // TODO notify game manager
    }
}
