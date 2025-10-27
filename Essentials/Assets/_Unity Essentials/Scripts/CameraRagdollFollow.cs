using UnityEngine;

public class CameraRagdollFollow : MonoBehaviour
{
    public Transform target;          // The player or hips the camera follows
    public Vector3 normalOffset = new Vector3(0, 2, -4);
    public Vector3 ragdollOffset = new Vector3(0, 4, -8); // pulled back for visibility
    public float smoothSpeed = 5f;
    public bool isRagdoll = false;    // toggled by RagdollController

    private Vector3 currentOffset;

    void Start()
    {
        currentOffset = normalOffset;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Choose which offset to use
        Vector3 desiredOffset = isRagdoll ? ragdollOffset : normalOffset;

        // Smoothly move offset to avoid jerky changes
        currentOffset = Vector3.Lerp(currentOffset, desiredOffset, Time.deltaTime * 2f);

        // Follow the target
        Vector3 desiredPosition = target.position + currentOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Keep camera looking at the target
        transform.LookAt(target);
    }
}
