using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    //Changeable private values for explosion animation
    [SerializeField] private float explosionForce = 200f;

    [SerializeField] private float explosionRadius = 5f;

    [SerializeField] private float torqueForce = 150f;

    [SerializeField] private float destroyDelay = 3f;

    public void Break()
    {
        //For each fragment in the breakable wall add an explosion force to appear broken
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            rb.AddTorque(Random.insideUnitSphere * torqueForce);
        }
        
        //Change each brick's layer to Wall_Debris to ensure no interference with the player after collision
        foreach (Transform brick in transform)
        {
            brick.gameObject.layer = LayerMask.NameToLayer("Wall_Debris");
        }
        
        //Clean up debris
        Destroy(gameObject, destroyDelay);
    }
}
