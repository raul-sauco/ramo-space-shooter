using System.Collections; 
using UnityEngine;

/// <summary>
/// Points the direction for the level's destination.
/// https://www.youtube.com/watch?v=dHzeHh-3bp4
/// </summary>
public class Pointer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform origin;
    [SerializeField] private float borderSize = 50f;
    [SerializeField] private float smoothSpeed = 0.5f;

    private RectTransform pointerRectTransform;
    // Enemy position on the screen based on current camera.
    private Vector3 tp;
    private bool enemyVisible;

    // Start is called before the first frame update
    void Start()
    {
        GameObject targetGo = GameObject.FindWithTag("Boss");
        if (targetGo != null)
            target = targetGo.transform;
        else
            Debug.LogWarning("No enemy boss found, pointer will not work");
        GameObject originGo = GameObject.FindWithTag("Player");
        if (originGo != null)
            origin = originGo.transform;
        else
            Debug.LogWarning("No player found, pointer will not work");

        // Deactivate this game object if we could not configure it.
        if (target == null || origin == null)
            gameObject.SetActive(false);

        pointerRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Check if target is visible on screen
        tp = Camera.main.WorldToScreenPoint(target.position);
        enemyVisible = (tp.x >= borderSize || tp.x <= Screen.width - borderSize || 
            tp.y >= borderSize || tp.y <= Screen.height - borderSize);
            
        if (enemyVisible)
        {
            // Point towards the enemy boss.
            Vector3 dir = (target.position - origin.position).normalized;
            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
            pointerRectTransform.localEulerAngles = new Vector3(0,0, angle);

            // Create a copy of the target screen position inside the screen.
            Vector3 ctp = tp;
            if (ctp.x <= borderSize) ctp.x = borderSize;
            if (ctp.x >= Screen.width - borderSize) ctp.x = Screen.width - borderSize;
            if (ctp.y <= borderSize) ctp.y = borderSize;
            if (ctp.y >= Screen.height - borderSize) ctp.y = Screen.height - borderSize;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(ctp);
            pointerRectTransform.position = worldPosition;
        }
    }
}
