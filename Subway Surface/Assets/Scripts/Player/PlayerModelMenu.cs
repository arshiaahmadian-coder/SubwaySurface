using UnityEngine;

public class PlayerModelMenu : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator.SetTrigger("Idle");
    }
}
