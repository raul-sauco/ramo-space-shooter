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
    // Keep a reference to unsubscribe.
    private Boss bossScript;
     
   #region lifecycle

    // Start is called before the first frame update
    void Start()
    {
        // The parent canvas uses screen space camera, assign the current camera.
        Canvas canvas = transform.parent.gameObject.GetComponent<Canvas>();
        if (canvas != null)
            canvas.worldCamera = Camera.main;
        else
            Debug.LogWarning("Canvas component found");
            
        GameObject targetGo = GameObject.FindWithTag("Boss");
        if (targetGo != null)
        {
            target = targetGo.transform;
            SubscribeToBossDestroyedEvent(targetGo);
        }
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
        // Check that the boss has not been destroyed.
        if (tp != null)
            UpdatePointer();
    }

    // Clean up before the object is disabled.
    void OnDisable()
    {
        if (bossScript != null)
            bossScript.OnDestroyed -= BossDestroyedCallback;
    }

    #endregion  // Lifecycle

    private void UpdatePointer()
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

    #region events

    // We want to know when the scene's boss is destroyed.
    // Each scene can only have one GameObject tagged "Boss"
    void SubscribeToBossDestroyedEvent(GameObject bossGo)
    {
        bossScript = bossGo.GetComponent<Boss>();
        if (bossScript != null)
            bossScript.OnDestroyed += BossDestroyedCallback;
        else
            Debug.LogWarning("Boss GameObject does not have Boss script component");
    }

    // The default behaviour is to disable the arrow when the boss is destroyed.
    private void BossDestroyedCallback()
    {
        gameObject.SetActive(false);
    }

    #endregion // events
}
