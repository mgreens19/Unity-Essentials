using UnityEngine;
using System.Collections;
public class PlayerHitReaction : MonoBehaviour
{
    public Rigidbody rb; // assign in Inspector
    public float hitForce = 10f;

    public void OnHitByCar()
    {
        rb.isKinematic = false; // enable physics if normally off
        rb.AddForce(Vector3.up * 5f + Vector3.back * hitForce, ForceMode.Impulse); // funny bounce
        // optionally trigger Game Over
        StartCoroutine(GameOver());
    }


    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Game Over!");
    }
}
