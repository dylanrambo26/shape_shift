using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Define totalAttempts with a public get but a private set and initialize to 1 because player is on first attempt.
    public int TotalAttempts { get; private set; } = 1;

    public void IncrementAttempts()
    {
        TotalAttempts++;
    }
    
    //Change gravity to double that of normal. Can be changed later.
    private void Start()
    {
        Physics.gravity = new Vector3(0, -20f, 0);
    }
}
