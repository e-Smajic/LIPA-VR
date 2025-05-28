using UnityEngine;

public class ShopAssistant : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartTalking()
    {
        animator.SetBool("IsTalking", true);
    }

    public void StopTalking()
    {
        animator.SetBool("IsTalking", false);
    }
}
