using UnityEngine;

public class PetAnimations : MonoBehaviour
{
    public Animator animator;
    public void RandomizeAnimation()
    {
        float random = Random.Range(0f, 1f);

        animator.Play(
            animator.GetCurrentAnimatorStateInfo(0).shortNameHash,
            0,
            random
        );
    }
}
