using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject onCollectEffect;

    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Tell the manager that one collectible is gone
            if (CollectibleManager.instance != null)
                CollectibleManager.instance.OnCollectibleCollected();

            // Spawn pickup effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);

            // Destroy this collectible
            Destroy(gameObject);
        }
    }
}
