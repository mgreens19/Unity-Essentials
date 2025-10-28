using UnityEngine;

public class CarHit : MonoBehaviour
{
    [Header("Impact Settings")]
    public float hitForce = 1500f;   // how hard to fling
    public float upwardForce = 300f; // extra lift


    private void OnCollisionEnter(Collision collision)
    {
        // If the car hits the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Find their ragdoll controller
            RagdollController ragdoll = collision.gameObject.GetComponent<RagdollController>();
            if (ragdoll != null)
            {
                ragdoll.SetRagdoll(true); // enable ragdoll mode

                // Pick a contact point and direction
                ContactPoint contact = collision.contacts[0];
                Vector3 hitDirection = (contact.point - transform.position).normalized;

                // Try to get a Rigidbody from the player (root or hips)
                Rigidbody playerRoot = collision.rigidbody;
                if (playerRoot == null)
                {
                    // if root doesn’t have one, try the hips
                    var hips = ragdoll.hipsBone;
                    if (hips != null)
                        playerRoot = hips.GetComponent<Rigidbody>();
                }

                // Apply force away from the car + a bit upward
                if (playerRoot != null)
                {
                    Vector3 launchDirection = (hitDirection + Vector3.up * 0.3f).normalized;
                    playerRoot.AddForce(launchDirection * hitForce + Vector3.up * upwardForce);
                }
            }
        }
    }
}