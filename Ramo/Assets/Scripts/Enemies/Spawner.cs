using System.Collections; 
using UnityEngine;
using Pathfinding;

/// <summary>
/// Enemy spawner randomly creates enemies in front of the player.
/// Inspired by https://www.c-sharpcorner.com/article/spawning-random-enemies-at-random-time-and-positions/
/// </summary>
public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] float offsetX;
    [SerializeField] float maxDelay;
    [SerializeField] float minDelay;
    [SerializeField] int startWait;
    [SerializeField] bool stop;

    // The spawner only randomizes on y axis
    private float rangeY;
    private float spawnWait;
    private Transform target;
  
    int randEnemy;
    
    void Start()  
    {  
        GameObject playerGo = PlayerState.Instance.gameObject;
        if (playerGo != null)
        {
            target = playerGo.transform;
        } else 
            Debug.LogWarning("Could not find player object");
        StartCoroutine(Spawn());
        rangeY = transform.localScale.y / 2;
    }  
  
    
    void Update()  
    {  
        spawnWait = Random.Range(minDelay, maxDelay);
        transform.position = new Vector3(target.position.x + offsetX, target.position.y, 0);
    }  
  
    private IEnumerator Spawn()  
    {  
        yield return new WaitForSeconds (startWait);  
  
        while (!stop)  
        {  
            randEnemy = CalculateWeightedRandomEnemyType();
            Vector3 spawnPosition = new Vector3(0f, Random.Range(-rangeY, rangeY), 0f);
            GameObject enemy = Instantiate(
                enemies[randEnemy], 
                spawnPosition + transform.TransformPoint(0, 0, 0), 
                gameObject.transform.rotation
            );
            // Capsule type enemies use A* pathfinding, assign a setter.target.
            if (randEnemy == 0)
            {
                AIDestinationSetter setter = enemy.GetComponent<AIDestinationSetter>();
                if (setter != null)
                {
                    setter.target = target;
                }
            } else 
            {
                // TODO Get the enemy script and assign the target.
            }
            yield return new WaitForSeconds(spawnWait);
        }  
    }

    private int CalculateWeightedRandomEnemyType()
    {
        // 20% chance getting a sphere type enemy, 80% a capsule.
        var rand = Random.Range(0,100);
        return rand > 20 ? 0 : 1;
    }
} 