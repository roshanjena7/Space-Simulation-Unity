using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    public float distance = 40f;

    float angle = 0;

    void Update()
    {
        angle += speed * Time.deltaTime;

        float rad = angle * Mathf.Deg2Rad;

        float x = distance * Mathf.Cos(rad);
        float z = distance * Mathf.Sin(rad);

        transform.position = target.position + new Vector3(x, 15, z);
        transform.LookAt(target);
    }
}