using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour
{
    public Transform satellite;
    public Transform earth;
    public Transform groundStation;

    public TextMeshProUGUI altitudeText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI signalText;
    public TextMeshProUGUI statusText;

    public Slider signalBar;

    private Vector3 lastPos;

    void Start()
    {
        lastPos = satellite.position;
    }

    void Update()
    {
        if (satellite == null || earth == null) return;

        // 📏 ALTITUDE
        float altitude = Vector3.Distance(satellite.position, earth.position);
        altitudeText.text = "ALT: " + altitude.ToString("F1") + " m";

        // 🚀 SPEED
        float speed = (satellite.position - lastPos).magnitude / Time.deltaTime;
        speedText.text = "SPD: " + speed.ToString("F1") + " m/s";

        // 📡 SIGNAL
        float signal = 0f;
        if (groundStation != null)
        {
            float dist = Vector3.Distance(satellite.position, groundStation.position);
            signal = Mathf.Clamp01(1f - dist / 20f);
        }

        signalText.text = "SIG: " + (signal * 100f).ToString("F0") + "%";

        if (signalBar != null)
            signalBar.value = signal;

        // 🛰 STATUS LOGIC
        if (altitude < 2f)
            statusText.text = "STATUS: CRASH";
        else if (signal < 0.2f)
            statusText.text = "STATUS: SIGNAL LOST";
        else if (altitude > 8f && altitude < 15f)
           statusText.text = "STATUS: STABLE ORBIT";
        else
            statusText.text = "STATUS: ADJUSTING...";

        lastPos = satellite.position;
    }
}