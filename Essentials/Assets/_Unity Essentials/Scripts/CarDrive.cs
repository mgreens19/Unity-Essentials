using UnityEngine;
using UnityEngine.EventSystems;

public class CarDrive : MonoBehaviour
{
    public Transform endPoint; // Drag in the other tunnel
    public float speed = 50f; // adjust as needed
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Calculate direction once at the start
        moveDirection = (endPoint.position - transform.position).normalized;
    }

    private Vector3 moveDirection;
    // Update is called once per frame
    void Update()
    {
        // Move toward the end tunnel.
        transform.position += moveDirection * speed * Time.deltaTime;

        // Face the direction it's moving
        transform.forward = moveDirection;

        // When it reaches other tunnel, destroy it. 
        if(Vector3.Distance(transform.position, endPoint.position) < 1f)
        {
            Destroy(gameObject);
        }

    }
}
