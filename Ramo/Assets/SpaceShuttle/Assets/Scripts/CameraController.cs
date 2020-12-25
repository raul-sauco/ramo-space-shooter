using UnityEngine;

public class CameraController : MonoBehaviour {

    const float LIMIT_Y_MIN = -50;
    const float LIMIT_Y_MAX = 50;

    public Transform targetlook;
    [Range(4, 20)]
    public float distance;
    [Range(0, 10)]
    public float sensitivityX;
    [Range(0, 10)]
    public float sensitivityY;

    private Vector2 currentXY = new Vector2(0,0);

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            distance++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            distance--;
        }

        distance = Mathf.Clamp(distance, 4, 20);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            currentXY.x += Input.GetAxis("Mouse X") * sensitivityX;
            currentXY.y -= Input.GetAxis("Mouse Y") * sensitivityY;

            currentXY.y = Mathf.Clamp(currentXY.y, LIMIT_Y_MIN, LIMIT_Y_MAX);
        }
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentXY.y, currentXY.x, 0);
        transform.position = targetlook.position + rotation * dir;
        transform.LookAt(targetlook.position);
    }
}
