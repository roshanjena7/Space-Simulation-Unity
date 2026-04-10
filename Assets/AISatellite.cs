using UnityEngine;

public class AISatellite : MonoBehaviour
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

        Vector3 targetPos = earth.position + new Vector3(x, 0, z);

        // AI smoothing (smart movement)
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 2f);
    }
}