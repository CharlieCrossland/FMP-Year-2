using UnityEngine;

public class ChaosAnimation : MonoBehaviour
{
    [Header("References")]
    public Animator animator;

    public void StartAnimation()
    {
        animator.SetTrigger("Start");
    }    
}
