using UnityEngine;

namespace Shmup
{
    public class ParallaxController : MonoBehaviour
    {
        [System.Serializable]
        public class ParallaxLayer
        {
            public Transform background;
            public float smoothing = 10f;
            public float multiplier = 15f;
        }

        [SerializeField] ParallaxLayer[] layers;

        Transform cam;
        float previousCamPosX;

        void Start()
        {
            cam = Camera.main.transform;
            previousCamPosX = cam.position.x;
        }

        void Update()
        {
            for (var i = 0; i < layers.Length; i++)
            {
                var parallax = (previousCamPosX - cam.position.x) * layers[i].multiplier;
                var targetX = layers[i].background.position.x + parallax;

                var targetPosition = new Vector3(targetX, layers[i].background.position.y, layers[i].background.position.z);

                layers[i].background.position = Vector3.Lerp(layers[i].background.position, targetPosition, layers[i].smoothing * Time.deltaTime);
            }

            previousCamPosX = cam.position.x;
        }
    }
}
