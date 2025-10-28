using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstaclePatterns : MonoBehaviour
{
    //List of prefab obstacle patterns for customizable order and repetitiveness
    public List<GameObject> obstaclePatterns;
    private int patternIndex = 0;
    private GameObject currentPattern;
    public bool spawnTriggered = false;
    
    //Spawn the first pattern in the list
    private void Start()
    {
        currentPattern = obstaclePatterns[patternIndex];
        Instantiate(currentPattern);
        patternIndex += 1;
    }
    
    //Spawn the next pattern in the obstacle list
    public void SpawnNextPattern()
    {
        //Spawn only one at a time and don't try to spawn when game is finished
        if (!spawnTriggered && patternIndex < obstaclePatterns.Count)
        {
            spawnTriggered = true;
            currentPattern = obstaclePatterns[patternIndex];
            Instantiate(currentPattern);
            patternIndex += 1;
        }
    }
    
    //Destroy all instances of the obstacles patterns and spawn from the beginning of the list again
    public void ResetSpawner()
    {
        foreach (var obstaclePattern in GameObject.FindGameObjectsWithTag("Obstacle Layer"))
        {
            Destroy(obstaclePattern);
        }

        patternIndex = 0;
        spawnTriggered = false;

        currentPattern = obstaclePatterns[patternIndex];
        Instantiate(currentPattern);
        patternIndex += 1;
    }
}
