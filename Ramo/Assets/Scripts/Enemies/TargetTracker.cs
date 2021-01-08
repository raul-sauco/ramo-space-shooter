using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks a target being able to keep a distance.
/// </summary>
public class TargetTracker : MonoBehaviour
{
    [SerializeField] private float minOffset = 20f;
    [SerializeField] private float maxOffset = 25f;
    [SerializeField] private float smoothSpeed = 0.1f;
    [SerializeField] private float health = 100f;
    [SerializeField] private float hitDamage = 5f;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject explossionPrefab;
    [SerializeField] private GameObject explossionPrefabSm;
    [SerializeField] private GameObject sphere;
    [SerializeField] private GameObject satellite;
    [SerializeField] private AudioSource explossionSfx;

    
    void Start()
    {
        GameObject playerGo = PlayerState.Instance.gameObject;
        if (playerGo != null)
        {
            target = playerGo.transform;
        } else
        {
            Debug.LogWarning("Could not find player object");
        }        
   }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Calculate if we need to run away or towards the target.
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist < minOffset || dist > maxOffset)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            Vector3 desiredPosition;
            if (dist < minOffset)
            {
                desiredPosition = transform.position - dir;
            } else
            {
                desiredPosition = transform.position + dir;
            }
            transform.position = Vector3.Lerp(transform.position, 
                desiredPosition, smoothSpeed);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "PlayerAttack")
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        health -= hitDamage;
        if (health < 0)
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        // Create a small explossion for the satellite
        GameObject explossionFxSm = Instantiate(explossionPrefabSm, 
            satellite.transform.position, satellite.transform.rotation);
        Destroy(explossionFxSm, 2f);
        Destroy(satellite);
        Destroy(sphere);
        explossionSfx.Play();
        GameObject explossionFx = Instantiate(explossionPrefab, 
            transform.position, transform.rotation);
        Destroy(explossionFx, 2f);
        Destroy(gameObject, 2f);
    }
}
