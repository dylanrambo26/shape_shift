using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private float explosionForce = 200f;

    [SerializeField] private float explosionRadius = 5f;

    [SerializeField] private float torqueForce = 150f;

    [SerializeField] private float destroyDelay = 3f;

    public void Break()
    {
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            rb.AddTorque(Random.insideUnitSphere * torqueForce);

            /*Collider collider = rb.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }*/
        }

        foreach (Transform brick in transform)
        {
            brick.gameObject.layer = LayerMask.NameToLayer("Wall_Debris");
        }
        
        Destroy(gameObject, destroyDelay);
    }
}
