using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private GameObject explossionPrefab;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
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
