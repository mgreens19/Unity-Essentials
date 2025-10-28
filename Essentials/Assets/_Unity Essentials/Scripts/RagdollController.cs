using StarterAssets;
using UnityEditor;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public Transform hipsBone; // Drag your hips/spine bone here
    public Transform cameraRoot; // Drag PlayerCameraRoot here
    public GameObject playerFollowCamera;

    private Rigidbody[] ragdollBodies;
    private Animator animator;
    public MonoBehaviour ThirdPersonController;



    void Start()
    {
        animator = GetComponent<Animator>();
        ragdollBodies = GetComponentsInChildren<Rigidbody>();

        // Try to find a controller automatically
        ThirdPersonController = GetComponent<MonoBehaviour>();

        SetRagdoll(false); // start normal
    }

    public void SetRagdoll(bool isRagdoll)
    {
        // Loop through every Rigidbody in the character's body.
        // Each limb (like arms, legs, etc.) has its own Rigidbody.
        foreach (Rigidbody rb in ragdollBodies)
        {
            // Skip the main object (the root) — we don’t want to affect that one
            if (rb.gameObject != gameObject)
                // If isRagdoll = true → make rigidbodies active (not kinematic)
                // If isRagdoll = false → freeze them (kinematic)
                // "isKinematic" means the Rigidbody ignores physics.
                rb.isKinematic = !isRagdoll;
        }

        // The Animator moves the character’s bones for normal walking/running.
        // When we switch to ragdoll, we need to turn it OFF, 
        // otherwise it will fight the physics and try to pose the character.
        if (animator != null)
            animator.enabled = !isRagdoll;

        // The movement controller script lets the player move normally.
        // We disable it when ragdolling so the player can’t keep walking or jumping.
        if (ThirdPersonController != null)
            ThirdPersonController.enabled = !isRagdoll;


        // 🔹 Switch the camera follow target when ragdolling
        if (cameraRoot != null && hipsBone != null)
        {
            if (isRagdoll)
                cameraRoot.SetParent(hipsBone); // follow hips while ragdolled
            else
                cameraRoot.SetParent(transform); // reattach to player root when normal
        }

        // Turn off normal follow when ragdolled and enable orbit camera
       
        RagdollCameraOrbit orbit = Camera.main.GetComponent<RagdollCameraOrbit>();

        if (isRagdoll)
        {
            
            if (orbit != null) orbit.Activate(hipsBone);
        }
        else
        {
           
            if (orbit != null) orbit.Deactivate();
        }

        if (isRagdoll)
        {
            if (playerFollowCamera != null) playerFollowCamera.SetActive(false);
            if (orbit != null) orbit.Activate(hipsBone);
        }
        else
        {
            if (playerFollowCamera != null) playerFollowCamera.SetActive(true);
            if (orbit != null) orbit.Deactivate();
        }
    }

#if UNITY_EDITOR
#endif

#if UNITY_EDITOR
    [CustomEditor(typeof(RagdollController))]
    public class RagdollControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            RagdollController controller = (RagdollController)target;

            GUILayout.Space(10);
            if (GUILayout.Button("Activate Ragdoll"))
            {
                controller.SetRagdoll(true);
            }
            if (GUILayout.Button("Deactivate Ragdoll"))
            {
                controller.SetRagdoll(false);
            }
        }
    }
}
#endif

