using UnityEngine;
using UnityEngine.UI;

public class SignalUI : MonoBehaviour
{
    public Transform satellite;
    public Transform groundStation;

    public float maxRange = 15f;

    public Slider signalSlider;
    public Image fillImage;

    void Update()
    {
        if (satellite == null || groundStation == null) return;

        float distance = Vector3.Distance(satellite.position, groundStation.position);

        float strength = 1 - (distance / maxRange);
        strength = Mathf.Clamp01(strength);

        signalSlider.value = strength;

        // Color change
        fillImage.color = Color.Lerp(Color.red, Color.green, strength);
    }
}