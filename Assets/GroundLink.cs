using UnityEngine;

public class GroundLink : MonoBehaviour
{
    public Transform ground;
    LineRenderer line;

    void Start()
    {
        // ✅ Use existing LineRenderer
        line = GetComponent<LineRenderer>();

        // Safety check
        if (line == null)
        {
            line = gameObject.AddComponent<LineRenderer>();
        }

        line.positionCount = 2;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;

        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.red;
        line.endColor = Color.red;
    }

    void Update()
    {
        // ❗ Prevent crash
        if (ground == null) return;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, ground.position);
    }
}