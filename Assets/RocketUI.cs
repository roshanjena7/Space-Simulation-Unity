using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RocketUI : MonoBehaviour
{
    [Header("References")]
    public Transform satellite;
    public Transform earth;
    public Transform groundStation;

    [Header("UI")]
    public TextMeshProUGUI altitudeText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI signalText;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI orbitStatusText;
    public Image signalFill;

    [Header("Signal")]
    public float maxSignalRange = 500f;

    private Vector3 lastPosition;

    void Start()
    {
        if (satellite != null)
            lastPosition = satellite.position;
    }

    void Update()
    {
        // 🛑 GLOBAL SAFETY CHECK (prevents ALL null errors)
        if (satellite == null || earth == null)
            return;

        // =========================
        // 📏 ALTITUDE
        // =========================
        if (altitudeText != null)
        {
            float altitude = Vector3.Distance(satellite.position, earth.position);
            altitudeText.text = "ALT: " + altitude.ToString("F1") + " m";
        }

        // =========================
        // 🚀 SPEED
        // =========================
        Vector3 velocity = (satellite.position - lastPosition) / Time.deltaTime;
        float speed = velocity.magnitude;

        if (speedText != null)
            speedText.text = "SPD: " + speed.ToString("F1");

        lastPosition = satellite.position;

        // =========================
        // 📡 SIGNAL SYSTEM
        // =========================
        if (groundStation != null)
        {
            float distance = Vector3.Distance(satellite.position, groundStation.position);
            float signalStrength = Mathf.Clamp01(1f - (distance / maxSignalRange));
            float signalPercent = signalStrength * 100f;

            if (signalText != null)
                signalText.text = "SIG: " + Mathf.Round(signalPercent) + "%";

            if (signalFill != null)
                signalFill.fillAmount = signalStrength;

            // ✅ SAFE COLOR + STATUS
            if (signalPercent > 60f)
            {
                if (statusText != null)
                {
                    statusText.text = "STATUS: STRONG SIGNAL";
                    statusText.color = Color.green;
                }

                if (signalFill != null)
                    signalFill.color = Color.green;
            }
            else if (signalPercent > 20f)
            {
                if (statusText != null)
                {
                    statusText.text = "STATUS: WEAK SIGNAL";
                    statusText.color = Color.yellow;
                }

                if (signalFill != null)
                    signalFill.color = Color.yellow;
            }
            else
            {
                if (statusText != null)
                {
                    statusText.text = "STATUS: SIGNAL LOST";
                    statusText.color = Color.red;
                }

                if (signalFill != null)
                    signalFill.color = Color.red;
            }
        }

        // =========================
        // 🛰 ORBIT STABILITY (SAFE)
        // =========================
        if (orbitStatusText != null && velocity.magnitude > 0.01f)
        {
            Vector3 toEarth = (earth.position - satellite.position).normalized;
            Vector3 velocityDir = velocity.normalized;

            float alignment = Mathf.Abs(Vector3.Dot(velocityDir, toEarth));

            if (alignment < 0.2f)
            {
                orbitStatusText.text = "ORBIT: STABLE";
                orbitStatusText.color = Color.green;
            }
            else if (alignment < 0.5f)
            {
                orbitStatusText.text = "ORBIT: DRIFTING";
                orbitStatusText.color = Color.yellow;
            }
            else
            {
                orbitStatusText.text = "ORBIT: UNSTABLE";
                orbitStatusText.color = Color.red;
            }
        }
    }
}