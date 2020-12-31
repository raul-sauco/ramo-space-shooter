using UnityEngine;

public class Capsule : MonoBehaviour
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
        Debug.Log("Collided with " + collider.gameObject.tag);
        if (collider.gameObject.tag == "PlayerAttack" && isActive) {
            isActive = false;
            transform.localScale = new Vector3(0,0,0);
            GameObject halo = transform.Find("Halo").gameObject;
            if (halo != null)
                Destroy(halo);
            explossionFx = Instantiate(explossionPrefab, transform.position, transform.rotation);
            Invoke(nameof(DestroyExplossionFx), 2);
        }
    }

    private void DestroyExplossionFx()
    {
        Destroy(explossionFx);
        Destroy(gameObject);
    }
}
