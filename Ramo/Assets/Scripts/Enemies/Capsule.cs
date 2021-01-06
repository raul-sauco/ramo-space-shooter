using UnityEngine;

public class Capsule : MonoBehaviour
{
    [SerializeField] private GameObject explossionPrefab;
    [SerializeField] private AudioSource explossionSfx;

    private bool isActive;
    private GameObject explossionFx;

    void Start()
    {
        isActive = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (isActive)
        {
            var tag = collider.gameObject.tag;
            if (tag == "PlayerAttack" || tag == "Player")
            {
                isActive = false;
                transform.localScale = new Vector3(0,0,0);
                // Avoid collisions with the player
                transform.position += new Vector3(0f, 0f, 5f);
                GameObject halo = transform.Find("Halo").gameObject;
                if (halo != null)
                {
                    Destroy(halo);
                }
                explossionSfx.Play();    
                explossionFx = Instantiate(explossionPrefab, transform.position, transform.rotation);
                Destroy(explossionFx, 2f);
                Destroy(gameObject, 2.5f);
            }
        }
        if (collider.gameObject.tag == "PlayerAttack" && isActive) {
        }
    }
}
