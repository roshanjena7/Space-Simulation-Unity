using UnityEngine;

public class ISSOrbit : MonoBehaviour
{
    public Transform earth;

    float angle = 0;

    void Update()
    {
        if (earth == null) return;

        // ISS real speed (~7.66 km/s scaled)
        angle += 50f * Time.deltaTime;

        float radius = 18f;

        // ISS inclination = 51.6°
        float inc = 51.6f * Mathf.Deg2Rad;

        float rad = angle * Mathf.Deg2Rad;

        float x = radius * Mathf.Cos(rad);
        float z = radius * Mathf.Sin(rad);
        float y = z * Mathf.Sin(inc);

        transform.position = earth.position + new Vector3(x, y, z);
    }
}