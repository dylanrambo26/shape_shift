using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    [SerializeField] private float speed;
    private bool isObstaclePattern = false;
    private SpawnObstaclePatterns spawnObstaclePatternsScript;
    private bool hasSpawnedNext = false;

    private void Start()
    {
        spawnObstaclePatternsScript = GameObject.Find("Obstacle Spawner").GetComponent<SpawnObstaclePatterns>();
    }

    //Check if object is an obstacle pattern at instantiation
    void Awake()
    {
        if (gameObject.CompareTag("Obstacle Layer"))
        {
            isObstaclePattern = true;
            //speed = 4;
        }
    }

    
    void FixedUpdate()
    {
        //Move gameObject left
        transform.Translate(Vector3.left * (Time.deltaTime * speed));
        //If the object is an obstacle pattern it will be destroyed past the threshold of x = -14
        if (isObstaclePattern && transform.position.x < -14)
        {
            spawnObstaclePatternsScript.spawnTriggered = false;
            Destroy(gameObject);
        }
        
        //Spawn the next obstacle pattern
        if (isObstaclePattern && !hasSpawnedNext && transform.position.x < 13)
        {
            spawnObstaclePatternsScript.SpawnNextPattern();
            hasSpawnedNext = true;
        }
    }
    
}
