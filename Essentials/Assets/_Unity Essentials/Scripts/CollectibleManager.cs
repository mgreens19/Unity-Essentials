using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager instance; // Singleton access
    public GameObject player; // Drag player in Inspector
    public GameObject celebrationEffect; // Drag confetti prefab here

    private int totalCollectibles;

    void Awake()
    {
        // Basic singleton setup
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Count how many collectibles exist at the start
        totalCollectibles = Object.FindObjectsByType<Collectible>(FindObjectsSortMode.None).Length;

    }

    public void OnCollectibleCollected()
    {
        totalCollectibles--;

        // Check if all are collected
        if (totalCollectibles <= 0)
        {
            Celebrate();
        }
    }

    void Celebrate()
    {
        Vector3 playerPos = player.transform.position;
        // Spawn a few confetti effects in a circle around the player
        for (int i = 0; i < 5; i++)
        {
            Vector3 offset = UnityEngine.Random.insideUnitSphere * 2f; // spread around player
            offset.y = Mathf.Abs(offset.y); // make sure they appear above ground
            Instantiate(celebrationEffect, playerPos + offset, Quaternion.identity);
        }
       
        Debug.Log("All collectibles collected! Celebration triggered!");
    }

    // Optional helper method for UI counter
    public int GetRemainingCollectibles()
    {
        return totalCollectibles;
    }
}
