using UnityEngine;

namespace Shmup
{
    public class ParallaxController : MonoBehaviour
    {
        [SerializeField] Transform[] backgrounds; // Array of background layers
        [SerializeField] float smoothing = 10f;   // How smooth the parallax effect is
        [SerializeField] float speed = 2f;        // Speed of background scrolling
        [SerializeField] float multiplier = 15f; // How much the parallax effect increments per layer

        void Update()
        {
            // Iterate through each background layer
            for (var i = 0; i < backgrounds.Length; i++)
            {
                // Calculate the amount of movement for this frame
                var movement = speed * (i * multiplier) * Time.deltaTime;

                // Create a new target position vector
                var targetPosition = new Vector3(
                    backgrounds[i].position.x - movement, // Move to the left
                    backgrounds[i].position.y,
                    backgrounds[i].position.z
                );

                // Smoothly transition to the target position
                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPosition, smoothing * Time.deltaTime);
            }
        }
    }
}
