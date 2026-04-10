using UnityEngine;
using UnityEngine.UI;

public class SignalGlow : MonoBehaviour
{
    public Image fillImage;   // drag Fill here
    public float pulseSpeed = 2f;

    private float baseAlpha = 1f;

    void Update()
    {
        if (fillImage == null) return;

        float signal = fillImage.fillAmount;

        Color c = fillImage.color;

        // 🚀 STRONG SIGNAL → steady glow
        if (signal > 0.6f)
        {
            c.a = Mathf.Lerp(c.a, 1f, Time.deltaTime * 5f);
        }
        // ⚠ MEDIUM → slow pulse
        else if (signal > 0.2f)
        {
            c.a = 0.5f + Mathf.Sin(Time.time * pulseSpeed) * 0.3f;
        }
        // 🚨 LOW → fast blinking
        else
        {
            c.a = Mathf.PingPong(Time.time * 3f, 1f);
        }

        fillImage.color = c;
    }
}