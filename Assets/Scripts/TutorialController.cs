using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public int TotalAttempts { get; private set; } = 1;

    public void IncrementAttempts()
    {
        TotalAttempts++;
    }

    private void Start()
    {
        Physics.gravity = new Vector3(0, -20f, 0);
    }
}
