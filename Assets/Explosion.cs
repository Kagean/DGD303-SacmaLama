using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        // Animasyon uzunlu�u kadar s�re sonra objeyi yok et
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
