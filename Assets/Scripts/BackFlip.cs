using UnityEngine;
using UnityEngine.InputSystem;
public class BackFlip : MonoBehaviour
{
    [SerializeField] private PlayerInput playerinput;
    [SerializeField] private PetVariable anger;
    [SerializeField] private PetVariable energy;
    [SerializeField] private Animator animator;
    public void OnJump(InputValue value)
    {
        if (anger.value >= 0.5f || energy.value <= 0.5f) return;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Backflip")) return;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("BurgerEat")) return;
        
        animator.Play("Backflip");
    }
}
