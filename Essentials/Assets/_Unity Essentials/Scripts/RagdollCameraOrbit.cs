using UnityEngine;

public class RagdollCameraOrbit : MonoBehaviour
{
    public Transform target; // the ragdolled player (hips or root)
    public float distance = 8f; // starting zoom distance
    public float zoomSpeed = 2f;
    public float minDistance = 3f;
    public float maxDistance = 15f;
    public float orbitSpeed = 100f;

    private float yaw = 0f;
    private float pitch = 20f;
    private bool active = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        if (!active || target == null) return;

        // Scroll wheel zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Right click hold to orbit
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * orbitSpeed * 0.5f * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, -10f, 70f);
        }

        // Calculate position around target
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 direction = rotation * new Vector3(0, 0, -distance);
        transform.position = target.position + direction + Vector3.up * 1.5f;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }

    public void Activate(Transform followTarget)
    {
        target = followTarget;
        active = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Deactivate()
    {
        active = false;
    }
}
