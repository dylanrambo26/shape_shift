using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFireballAnimation : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    //Play expand animation for the current fireball inside the color gate
    public void PlayAnimation()
    {
        animator.SetTrigger("ColorGateHit");
    }
}
