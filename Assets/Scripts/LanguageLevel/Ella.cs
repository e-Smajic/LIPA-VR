using UnityEngine;

public class Ella : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.LookAt(player);
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
