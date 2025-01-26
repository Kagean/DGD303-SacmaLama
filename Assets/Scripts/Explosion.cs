using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        // Animasyon uzunluðu kadar süre sonra objeyi yok et
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
