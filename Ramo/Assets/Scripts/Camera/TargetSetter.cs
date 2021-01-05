using UnityEngine;
using Cinemachine;

/// <summary>
/// Sets the target of the virtual camera to the currently active player.
/// </summary>
public class TargetSetter : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            GameObject playerGo = PlayerState.Instance.gameObject;
            if (playerGo != null)
            {
                virtualCamera.Follow = playerGo.transform;
            } else 
                Debug.LogWarning("Could not find player object");
        } else 
            Debug.LogWarning("Could not find virtual camera component");
    }
}
