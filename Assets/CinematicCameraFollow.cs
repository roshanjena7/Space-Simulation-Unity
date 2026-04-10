using UnityEngine;

public class CinematicCameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // Satellite / Rocket

    [Header("Follow Settings")]
    public Vector3 offset = new Vector3(0, 3, -8);
    public float smoothSpeed = 2f;

    [Header("Look Settings")]
    public float lookSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // 🎯 Desired position
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // 🎬 Smooth movement
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 👀 Smooth look at target
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
    }
}