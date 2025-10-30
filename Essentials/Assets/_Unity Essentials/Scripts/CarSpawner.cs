using UnityEngine;
using System.Collections; 


public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public Transform startPoint;
    public Transform endPoint;
    public float minSpawnInterval = 1f;  // Smallest delay
    public float maxSpawnInterval = 5f;  // Largest delay

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnCars());
    }

    IEnumerator SpawnCars()
    {
        while (true)
        {
            // Wait for a random time between min and max
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            // Spawn the car
            GameObject car = Instantiate(carPrefab, startPoint.position, startPoint.rotation);
            CarDrive drive = car.GetComponent<CarDrive>();
            drive.endPoint = endPoint;
        }
    }
}