using UnityEngine;

namespace Shmup
{
    public class LerpColor : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private float[] hues = { 0f, 240f / 360f, 120f / 360f }; // K�rm�z�, Mavi, Ye�il
        private int currentHueIndex = 0;
        private int nextHueIndex = 1;
        private float colorChangeDuration = 2f;
        private float colorChangeTimer = 0f;
        private float saturation = 1f;
        private float value = 0.7f;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Renk ge�i�ini sa�la
            colorChangeTimer += Time.deltaTime;
            float t = Mathf.PingPong(colorChangeTimer / colorChangeDuration, 1f);
            float currentHue = Mathf.Lerp(hues[currentHueIndex], hues[nextHueIndex], t);
            spriteRenderer.color = Color.HSVToRGB(currentHue, saturation, value);

            // Renk ge�i�i tamamland���nda bir sonraki renge ge�
            if (t >= 0.99f)
            {
                colorChangeTimer = 0f;
                currentHueIndex = nextHueIndex;
                nextHueIndex = (nextHueIndex + 1) % hues.Length;
            }
        }
    }
}
