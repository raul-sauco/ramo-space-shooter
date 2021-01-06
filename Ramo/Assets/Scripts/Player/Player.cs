using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the player character.
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject explossionPrefab;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private float health = 100f;
    [SerializeField] private float capsuleHitDamage = 10f;
    [SerializeField] private float bossBulletHitDamage = 2f;

    private bool isActive;
    private GameObject explossionFx;
    private float startHealth;

    void Start()
    {
        isActive = true;
        startHealth = health;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (isActive) {
            var tag = collider.gameObject.tag;
            if (tag == "EnemyBullet") 
            {
                health -= bossBulletHitDamage;
            } else if (tag == "EnemyCapsule")
            {
                health -= capsuleHitDamage;
            }
            if (health > 0)
            {
                float remainingHealth = health / startHealth;
                healthBar.transform.localScale = new Vector3(remainingHealth, 1f, 1f);
                ColorizeHealthBar(remainingHealth);
            } else
            {
                DestroySelf();
            }
        }
    }

    // Destroy the player character.
    private void DestroySelf()
    {
        isActive = false;
        Vector3 position = new Vector3(transform.position.x, 
            transform.position.y, transform.position.z - 4);
        transform.localScale = new Vector3(0,0,0);
        explossionFx = Instantiate(explossionPrefab, position, transform.rotation);
        Destroy(explossionFx, 2f);
        Destroy(gameObject, 2.5f);
        // Todo notify game.
    }

    private void ColorizeHealthBar(float remainingHealth)
    {
        // Do nothing while above first threshold
        if (remainingHealth < 0.5 && remainingHealth > 0.2)
        {
            healthBar.GetComponent<Image>().color = new Color32(255, 140, 0, 255);
        } else if (remainingHealth < 0.2)
        {
            healthBar.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        } else if (remainingHealth >= 0.5)
        {
            // Prepare for power-ups and health-restore bonus in the future.
            healthBar.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
        }
    }
}
