using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerExplosion : MonoBehaviour
{
    //The playerExplosion animation uses rigidbody physics to simulate the player object fracturing into smaller pieces
    
    //fracturedMesh is a child mesh of the parent shape object
    [SerializeField] private GameObject normalMesh;
    [SerializeField] private GameObject fracturedMesh;
    
    
    [SerializeField] private float explosionForce = 500f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float torqueForce = 150f;
    [SerializeField] private float cleanupDelay = 3f;

    //Start positions and values to reset the animation
    private Vector3[] originalFragmentPositions;
    private Quaternion[] originalFragmentRotations;
    private Rigidbody[] originalRigidbodies;

    private SpawnObstaclePatterns spawnObstaclePatterns;

    private void Start()
    {
        spawnObstaclePatterns = GameObject.FindWithTag("ObstacleSpawner").GetComponent<SpawnObstaclePatterns>();
    }

    //When the script is loaded take the original positions and rigidbodies of all of the fragments to reset them later
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

    //Play explosion animation
    public void ExplodePlayer()
    {
        //Deactivate the normal mesh and activate the fractured
        normalMesh.SetActive(false);
        fracturedMesh.SetActive(true);
        
        //Set an explosion force for each fragment
        foreach (Rigidbody rb in originalRigidbodies)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            rb.AddTorque(Random.insideUnitSphere * torqueForce);
        }
        
        //Pause background movement and reset the fragments at the same time.
        StartCoroutine(PauseBackgroundMovement());
        StartCoroutine(ResetFragments());
    }
    
    //Pause background movement and obstacles to provide the appearance that the player has stopped moving.
    private IEnumerator PauseBackgroundMovement()
    {
        MoveLeft.isPaused = true;
        BackgroundScroller.isPaused = true;
        yield return new WaitForSeconds(cleanupDelay);
        spawnObstaclePatterns.ResetSpawner();
        MoveLeft.isPaused = false;
        BackgroundScroller.isPaused = false;
    }
    
    //Reset each fragment to its original state. Restore active state of the normal player mesh.
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
