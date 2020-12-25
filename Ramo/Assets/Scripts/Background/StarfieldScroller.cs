using UnityEngine;

/// Controls the scrolling of the starfield background.
public class StarfieldScroller : MonoBehaviour
{
    [SerializeField] private float parallax = .001f;

    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material material = mr.material;
        Vector2 offset = material.mainTextureOffset;
        offset.x = transform.position.x / transform.localScale.x * parallax;
        offset.y = transform.position.y / transform.localScale.y * parallax;
        material.mainTextureOffset = offset;
    }
}
