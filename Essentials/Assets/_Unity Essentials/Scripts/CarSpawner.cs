using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public Transform startPoint;
    public Transform endPoint;
     public float spawnInterval = 3f; // Time (in seconds) between spawns

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start a repeating loop that spawns cars
        InvokeRepeating(nameof(SpawnCar), 0f, spawnInterval);
    }

    
    void SpawnCar()
    {
        GameObject car = Instantiate(carPrefab, startPoint.position, startPoint.rotation);
        CarDrive drive = car.GetComponent<CarDrive>();
        drive.endPoint = endPoint;
    }
}
