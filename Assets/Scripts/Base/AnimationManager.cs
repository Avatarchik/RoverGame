using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    
    public virtual void RequestAnimation(string animationId)
    {
        animator.Play(animationId);
    }
}
