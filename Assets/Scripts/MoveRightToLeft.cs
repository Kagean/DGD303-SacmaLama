using UnityEngine;

namespace Shmup
{
    public class MoveRightToLeft : MonoBehaviour
    {
        [SerializeField] float speed = 5f; // H�z� ayarlamak i�in bir de�i�ken
        [SerializeField] float waveFrequency = 1f; // Dalga frekans�n� ayarlamak i�in bir de�i�ken
        [SerializeField] float waveAmplitude = 1f; // Dalga genli�ini ayarlamak i�in bir de�i�ken
        [SerializeField] bool enableWaveMovement = true; // Dalga hareketini a��p kapatmak i�in bir de�i�ken

        private float initialY;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            initialY = transform.position.y;
        }

        // Update is called once per frame
        void Update()
        {
            // Nesneyi sa�dan sola do�ru hareket ettir
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            // Dalga hareketi ekle
            if (enableWaveMovement)
            {
                float newY = initialY + Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
        }
    }
}
