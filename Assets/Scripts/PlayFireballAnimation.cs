using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFireballAnimation : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No Animator found on " + gameObject.name);
        }
    }

    public void PlayAnimation()
    {
        animator.SetTrigger("ColorGateHit");
        /*if (animator != null)
        {
            animator.Play("Fireball_Expand", -1, 0f);
        }
        else
        {
            Debug.LogError("Animator reference is null on " + gameObject.name);
        }*/
    }
}
