using UnityEngine;

/// <summary>
/// Manges the bullets enemies and the player shoot
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] private Vector3 speed;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private GameObject explossionPrefab;

    // Let instantiators alter default speed and direction.
    public void SetSpeed(Vector3 updatedSpeed)
    {
        speed = updatedSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        string tag = collider.gameObject.tag;
        if (tag == "Enemy" || tag == "Boss")
        {
            GameObject exp = Instantiate(explossionPrefab, 
                transform.position, transform.rotation);
            Destroy(exp, 1);
            Destroy(gameObject);
        }
    }
}
