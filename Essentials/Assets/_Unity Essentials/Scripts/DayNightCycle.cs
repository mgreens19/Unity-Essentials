using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Tooltip("How many real-time seconds it takes for a full day to pass.")]
    public float dayLengthInSeconds = 120f; // default: 2 minutes per full cycle

    void Update()
    {
        // Calculate how much to rotate per second (360 degrees in one full day)
        float rotationPerSecond = 360f / dayLengthInSeconds;

        // Apply rotation around the X-axis to simulate the sun moving
        transform.Rotate(Vector3.right * rotationPerSecond * Time.deltaTime);
    }
}
