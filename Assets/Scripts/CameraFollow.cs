using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 2f;
    public float lookSpeed = 5f;

    [Header("🎬 Cinematic Effects")]
    public float shakeAmount = 0.2f;
    public float shakeDuration = 2f;
    public float zoomSpeed = 2f;
    public float maxZoomOut = -20f;

    private float shakeTimer = 0f;

    void Start()
    {
        shakeTimer = shakeDuration;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 🎥 FOLLOW
        Vector3 desiredPosition = target.position + offset;

        // 🎬 ZOOM OUT slowly
        if (offset.z > maxZoomOut)
        {
            offset.z -= zoomSpeed * Time.deltaTime;
        }

        // 🌋 CAMERA SHAKE (during launch)
        if (shakeTimer > 0)
        {
            desiredPosition += Random.insideUnitSphere * shakeAmount;
            shakeTimer -= Time.deltaTime;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 👀 LOOK AT TARGET
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
    }
}