using UnityEngine;

public class CarHit : MonoBehaviour
{
    public float hitForce = 1500f;
    public float upwardForce = 300f;

    private bool hasHitPlayer = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHitPlayer) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            hasHitPlayer = true;

            // try find ragdoll controller on player root (or parent)
            RagdollController ragdoll = collision.gameObject.GetComponent<RagdollController>();
            if (ragdoll == null)
                ragdoll = collision.gameObject.GetComponentInChildren<RagdollController>();

            if (ragdoll != null)
            {
                // compute a good hit direction — use contact normal (away from car)
                ContactPoint contact = collision.contacts[0];
                Vector3 hitDir = (contact.point - transform.position).normalized; // direction from car to contact

                // launch the ragdoll (this method ensures it only happens once)
                ragdoll.Launch(hitDir, hitForce, upwardForce);

                // --- prevent further collisions between this car and the ragdoll ---
                // disable this car's collider immediately so it can't keep touching ragdoll limbs
                Collider carCol = GetComponent<Collider>();
                if (carCol != null) carCol.enabled = false;

                // optionally, stop the car rigidbody so it doesn't keep nudging
                Rigidbody carRb = GetComponent<Rigidbody>();
                if (carRb != null)
                {
                    carRb.linearVelocity = Vector3.zero;
                    carRb.angularVelocity = Vector3.zero;
                    carRb.isKinematic = true; // freeze it in place (optional)
                }

                // optional: destroy or deactivate the car after a short delay
                // Destroy(gameObject, 1.5f);
            }
        }
    }
}
