using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform earth;
    public float speed = 20f;
    public float radius = 15f;

    float angle = 0;

    void Update()
    {
        if (earth == null) return;

        angle += speed * Time.deltaTime;

        float rad = angle * Mathf.Deg2Rad;

        float x = radius * Mathf.Cos(rad);
        float z = radius * Mathf.Sin(rad);

        transform.position = earth.position + new Vector3(x, 0, z);
    }
}