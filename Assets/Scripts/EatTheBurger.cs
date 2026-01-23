using UnityEngine;

public class EatTheBurger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void EatBurger()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Backflip")) return;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("BurgerEat")) return;

        animator.Play("BurgerEat");
    }
}
