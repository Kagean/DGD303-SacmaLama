using UnityEngine;

namespace Shmup
{
    public class MoveRightToLeft : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] float waveFrequency = 1f;
        [SerializeField] float waveAmplitude = 1f;
        [SerializeField] bool enableWaveMovement = true;

        private float initialY;


        void Start()
        {
            initialY = transform.position.y;
        }

        void Update()
        {

            transform.Translate(Vector3.left * speed * Time.deltaTime);


            if (enableWaveMovement)
            {
                float newY = initialY + Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
        }
    }
}
