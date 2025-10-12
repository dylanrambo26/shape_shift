using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstaclePatterns : MonoBehaviour
{
    public List<GameObject> obstaclePatterns;
    private int patternIndex = 0;
    private GameObject currentPattern;
    //private GameObject nextPattern;
    public bool spawnTriggered = false;
    private void Start()
    {
        currentPattern = obstaclePatterns[patternIndex];
        Instantiate(currentPattern);
        patternIndex += 1;
    }
    
    //Spawn the next pattern in the obstacle list
    public void SpawnNextPattern()
    {
        if (!spawnTriggered)
        {
            spawnTriggered = true;
            currentPattern = obstaclePatterns[patternIndex];
            Instantiate(currentPattern);
            patternIndex += 1;
        }
    }
}
