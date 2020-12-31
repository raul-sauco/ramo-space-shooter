using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject explossionPrefab;

    private bool isActive;
    private GameObject explossionFx;

    void Start()
    {
        isActive = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (isActive) {
            isActive = false;
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 4);
            transform.localScale = new Vector3(0,0,0);
            explossionFx = Instantiate(explossionPrefab, position, transform.rotation);
            Invoke(nameof(DestroyExplossionFx), 2);
        }
    }

    private void DestroyExplossionFx()
    {
        Destroy(explossionFx);
    }
}
