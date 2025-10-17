using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int TotalAttempts { get; private set; }

    public void IncrementAttempts()
    {
        TotalAttempts++;
    }
}
