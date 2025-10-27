using UnityEngine;
using System.Collections;
public class PlayerHitReaction : MonoBehaviour
{
    public Rigidbody rb; // assign in Inspector
    public float hitForce = 50f;

    public void OnHitByCar()
    {
        rb.isKinematic = false; // enable physics if normally off
        rb.AddForce(Vector3.up * 5f + Vector3.back * hitForce, ForceMode.Impulse); // funny bounce
    }

}
