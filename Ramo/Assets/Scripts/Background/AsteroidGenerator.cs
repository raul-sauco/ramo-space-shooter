using System.Collections;
using UnityEngine;

/// <summary>
/// Procedural asteroid generator configurable from the Unity editor.
/// </summary>
public class AsteroidGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] asteroids;
    [SerializeField] float maxDist = 100f;
    [SerializeField] float minDist = 60f;
    [SerializeField] float maxYOffset = 15f;
    [SerializeField] float minZOffset = 3f;
    [SerializeField] float maxZOffset = 9f;
    [SerializeField] float maxDelay = 20f;
    [SerializeField] float minDelay = 10f;
    [SerializeField] float maxTumble = 1f;
    [SerializeField] float minTumble = 0f;
    
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = PlayerState.Instance.gameObject;
        if (go != null)
        {
            target = go.transform;
            StartCoroutine(nameof(Spawn));
        } else
        {
            Debug.LogWarning("AsteroidGenerator could not find player");
        }
    }
  
    private IEnumerator Spawn()  
    {  
        while (target != null)  
        {
            int index = Random.Range(0, asteroids.Length);
            float distance = Random.Range(minDist, maxDist);
            Vector3 direction = (target.GetComponent<Rigidbody>().velocity).normalized;
            if (direction != Vector3.zero)
            {
                // Only generate if the player is moving.
                Vector3 spawnOffset = distance * direction;
                // Adjust offsets
                var y = Random.Range((-1 * maxYOffset), maxYOffset);
                // Get a random positive or negative Z clearing z=0 by the given offset.
                var z = Random.Range(minZOffset, maxZOffset) * 
                    (Random.value > 0.5 ? -1 : 1);
                spawnOffset += new Vector3(0, y, z);
                GameObject asteroid = Instantiate(
                    asteroids[index], 
                    target.transform.position + spawnOffset, 
                    gameObject.transform.rotation
                );
                RandomRotator rr = asteroid.GetComponent<RandomRotator>();
                if (rr != null)
                {
                    rr.SetTumble(Random.Range(minTumble, maxTumble));
                }
            }
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }  
    }
}
