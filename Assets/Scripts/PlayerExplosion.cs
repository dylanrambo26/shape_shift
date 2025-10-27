using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerExplosion : MonoBehaviour
{
    [SerializeField] private GameObject normalMesh;
    [SerializeField] private GameObject fracturedMesh;
    [SerializeField] private float explosionForce = 500f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float torqueForce = 150f;
    [SerializeField] private float cleanupDelay = 3f;

    
    private Vector3[] originalFragmentPositions;
    private Quaternion[] originalFragmentRotations;
    private Rigidbody[] originalRigidbodies;
    
    private void Awake()
    {
        originalRigidbodies = fracturedMesh.GetComponentsInChildren<Rigidbody>(true);
        originalFragmentPositions = new Vector3[originalRigidbodies.Length];
        originalFragmentRotations = new Quaternion[originalRigidbodies.Length];

        for (int i = 0; i < originalRigidbodies.Length; i++)
        {
            originalFragmentPositions[i] = originalRigidbodies[i].transform.localPosition;
            originalFragmentRotations[i] = originalRigidbodies[i].transform.localRotation;
        }
    }

    public void ExplodePlayer()
    {
        normalMesh.SetActive(false);
        fracturedMesh.SetActive(true);

        foreach (Rigidbody rb in originalRigidbodies)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            rb.AddTorque(Random.insideUnitSphere * torqueForce);
        }

        StartCoroutine(PauseBackgroundMovement());
        StartCoroutine(ResetFragments());
    }

    private IEnumerator PauseBackgroundMovement()
    {
        MoveLeft.isPaused = true;
        BackgroundScroller.isPaused = true;
        yield return new WaitForSeconds(cleanupDelay);
        MoveLeft.isPaused = false;
        BackgroundScroller.isPaused = false;
    }

    private IEnumerator ResetFragments()
    {
        yield return new WaitForSeconds(cleanupDelay);

        foreach (Rigidbody rb in originalRigidbodies)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        for(int i = 0; i < originalRigidbodies.Length; i++)
        {
            Transform t = originalRigidbodies[i].transform;
            t.localPosition = originalFragmentPositions[i];
            t.localRotation = originalFragmentRotations[i];
        }
        
        fracturedMesh.SetActive(false);
        normalMesh.SetActive(true);
    }
}
