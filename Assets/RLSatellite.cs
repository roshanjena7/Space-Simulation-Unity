using UnityEngine;

public class RLSatellite : MonoBehaviour
{
    [Header("References")]
    public Transform earth;

    [Header("Launch")]
    public float launchForce = 20f;

    [Header("Orbit")]
    public float gravity = 25f;
    public float orbitSpeed = 15f;

    private Vector3 velocity;
    private bool launched = false;

    void Start()
    {
        if (earth == null) return;

        // Start near Earth surface
        transform.position = earth.position + new Vector3(0, 6f, 0);

        velocity = Vector3.zero;
    }

    void Update()
    {
        if (earth == null) return;

        Vector3 toEarth = (earth.position - transform.position);
        float distance = toEarth.magnitude;
        Vector3 gravityDir = toEarth.normalized;

        // =========================
        // 🚀 LAUNCH (ONLY ONCE)
        // =========================
        if (!launched)
        {
            velocity += Vector3.up * launchForce * Time.deltaTime;
            
            if (distance > 8f)
            {
                launched = true;

                // give sideways velocity (IMPORTANT)
                Vector3 radial = (transform.position - earth.position).normalized;
                Vector3 tangent = new Vector3(-radial.z, 0, radial.x);

                velocity = tangent * orbitSpeed;
            }
        }

        // =========================
        // 🌍 GRAVITY (ALWAYS ON)
        // =========================
        velocity += gravityDir * gravity * Time.deltaTime;

        // =========================
        // 🛰 MOVE
        // =========================
        transform.position += velocity * Time.deltaTime;
    }
}