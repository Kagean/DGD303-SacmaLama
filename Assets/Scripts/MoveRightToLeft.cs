using UnityEngine;

namespace Shmup
{
    public class MoveRightToLeft : MonoBehaviour
    {
        [SerializeField] float speed = 5f; // Hýzý ayarlamak için bir deðiþken
        [SerializeField] float waveFrequency = 1f; // Dalga frekansýný ayarlamak için bir deðiþken
        [SerializeField] float waveAmplitude = 1f; // Dalga genliðini ayarlamak için bir deðiþken
        [SerializeField] bool enableWaveMovement = true; // Dalga hareketini açýp kapatmak için bir deðiþken

        private float initialY;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            initialY = transform.position.y;
        }

        // Update is called once per frame
        void Update()
        {
            // Nesneyi saðdan sola doðru hareket ettir
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
