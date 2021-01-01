using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject explossionPrefab;
    [SerializeField] private float offsetX = 20f;
    [SerializeField] private float smoothSpeed = 0.1f;
    [SerializeField] private float activationDistance = 40f;
    [SerializeField] private float totalLife = 100f;
    [SerializeField] private float hitDamage = 5f;

    private Transform playerTransform;
    private bool isActive;
    private GameObject explossionFx;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        GameObject go = GameObject.Find("Player");
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
                isActive = true;
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
            new Vector3(offsetX, 0f, 0f);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (isActive && collider.gameObject.tag == "PlayerAttack")
            TakeDamage();
    }

    private void TakeDamage()
    {
        totalLife -= hitDamage;
        Debug.Log("Remaining life " + totalLife);
        if (totalLife < 0)
            DestroySelf();
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
