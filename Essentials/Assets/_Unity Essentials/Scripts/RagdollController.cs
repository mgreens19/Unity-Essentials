using StarterAssets;
using UnityEditor;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody[] ragdollBodies;
    private Animator animator;
    private MonoBehaviour ThirdPersonController;


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
        foreach (Rigidbody rb in ragdollBodies)
        {
            if (rb.gameObject != gameObject) // don't include root
                rb.isKinematic = !isRagdoll;
        }

        if (animator != null)
            animator.enabled = !isRagdoll;

        if (ThirdPersonController != null)
            ThirdPersonController.enabled = !isRagdoll;
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
#endif

