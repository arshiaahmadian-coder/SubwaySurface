using UnityEngine;

public class PlayerModelMenu : MonoBehaviour
{
    public Animator animator;

    private void OnEnable()
    {
        animator.SetBool("Idle", true);
    }
}
