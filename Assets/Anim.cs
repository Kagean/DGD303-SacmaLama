using UnityEngine;

namespace Shmup
{
    public class Anim : MonoBehaviour
    {

        [SerializeField] private Animator animator;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("AttackTrigger");
            }

        }
    }
}
