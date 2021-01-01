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

    void Start()
    {
        StartCoroutine(nameof(Attack));
        Debug.Log("Boss attacking");
    }

    private void Shoot()
    {
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

        while(true) // TODO game is active?
        {
            Shoot();
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
        }
    }


}
