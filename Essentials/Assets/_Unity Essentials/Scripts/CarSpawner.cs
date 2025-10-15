using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public Transform startPoint;
    public Transform endPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnCar();
    }

    
    void SpawnCar()
    {
        GameObject car = Instantiate(carPrefab, startPoint.position, startPoint.rotation);
        CarDrive drive = car.GetComponent<CarDrive>();
        drive.endPoint = endPoint;
    }
}
