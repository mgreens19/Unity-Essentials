using UnityEngine;

public class CarHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // If the car hits the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Try to find the player's ragdoll controller
            RagdollController ragdoll = collision.gameObject.GetComponent<RagdollController>();
            if (ragdoll != null)
            {
               
                Rigidbody rb = collision.rigidbody; // or other.attachedRigidbody
                if (rb != null)
                {
                    rb.AddForce(transform.forward * 500f + Vector3.up * 200f);
                }
                ragdoll.SetRagdoll(true);

            }
        }
    }
}
