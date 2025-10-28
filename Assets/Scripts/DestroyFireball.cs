using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFireball : MonoBehaviour
{
    //Used to destroy the fireball after the animation is played
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
