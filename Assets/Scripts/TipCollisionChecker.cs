using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipCollisionChecker : MonoBehaviour
{
    private TutorialPause tutorialPauseScript;
    private void Start()
    {
        tutorialPauseScript = GameObject.FindGameObjectWithTag("TutorialManager").GetComponent<TutorialPause>();
    }

    private void Update()
    {
        if (tutorialPauseScript.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                tutorialPauseScript.HideTip();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        String patternName = other.transform.parent.gameObject.name;
        if (patternName == "Pattern1Tutorial(Clone)")
        {
            tutorialPauseScript.ShowTip("Left-Click to single jump with Square. Press enter to continue.");
        }
        else if (patternName == "Pattern2Tutorial(Clone)")
        {
            tutorialPauseScript.ShowTip("Left-Click to dash through breakable wall with Trapezoid. Press enter to continue.");
        }
    }
}
