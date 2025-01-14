using UnityEngine;

namespace Shmup
{
    public class ParallaxController : MonoBehaviour
    {
        [SerializeField] Transform[] backgrounds; // Array of background layers
        [SerializeField] float smoothing = 10f; // How smooth the parallax effect is
        [SerializeField] float multiplier = 15f; // How much the parallax effect increments per layer

        Transform cam;
        Vector3 previousCamPos;

        void Awake() => cam = Camera.main.transform;

        void Start() => previousCamPos = cam.position;
        void Update()
        {
            for (var i = 0; i < backgrounds.Length; i++)
            {
                var parallax = (previousCamPos.x - cam.position.x) * (i * multiplier);
                var targetX = backgrounds[i].position.x + parallax;

                var targetPosition = new Vector3(targetX, backgrounds[i].position.y, backgrounds[i].position.z);

                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPosition, smoothing * Time.deltaTime);
            }

            previousCamPos = cam.position;
        }
    }
}
