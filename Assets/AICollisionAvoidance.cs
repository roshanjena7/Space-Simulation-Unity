using UnityEngine;

public class AICollisionAvoidance : MonoBehaviour
{
    public Transform earth;
    public float speed = 20f;
    public float radius = 15f;

    public float avoidDistance = 2f;

    float angle = 0;

    void Update()
    {
        if (earth == null) return;

        angle += speed * Time.deltaTime;

        float rad = angle * Mathf.Deg2Rad;

        Vector3 targetPos = earth.position + new Vector3(
            radius * Mathf.Cos(rad),
            0,
            radius * Mathf.Sin(rad)
        );

        // 🧠 AI Collision Avoidance
        GameObject[] sats = GameObject.FindGameObjectsWithTag("Satellite");

        foreach (GameObject sat in sats)
        {
            if (sat == gameObject) continue;

            float dist = Vector3.Distance(transform.position, sat.transform.position);

            if (dist < avoidDistance)
            {
                // Push away
                Vector3 away = (transform.position - sat.transform.position).normalized;
                targetPos += away * 2f;
            }
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 2f);
    }
}