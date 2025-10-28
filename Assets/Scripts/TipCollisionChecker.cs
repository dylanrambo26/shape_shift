using System;
using UnityEngine;

public class TipCollisionChecker : MonoBehaviour
{
    private TutorialPause tutorialPauseScript;
    private void Start()
    {
        tutorialPauseScript = GameObject.FindGameObjectWithTag("TutorialManager").GetComponent<TutorialPause>();
    }
    
    //Continue tutorial after a tip when the player presses enter
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
    
    //Play the specific tip based on the trigger hit
    private void OnTriggerEnter(Collider other)
    {
        String tip = other.gameObject.tag;
        switch (tip)
        {
            case "Pattern1.1Tip":
                tutorialPauseScript.ShowTip("Left-Click to single jump with Square. Press enter to continue.");
                break;
            case "Pattern1.2Tip":
                tutorialPauseScript.ShowTip("Press SPACE to switch to Trapezoid. Press enter to continue.");
                break;
            case "Pattern2.1Tip":
            {
                tutorialPauseScript.ShowTip("Left-Click to dash through breakable walls with trapezoid. Press enter to continue.");
                break;
            }
            case "Pattern2.2Tip":
            {
                tutorialPauseScript.ShowTip("Press SPACE again to switch to Triangle. Press enter to continue.");
                break;
            }
            case "Pattern3.1Tip":
            {
                tutorialPauseScript.ShowTip("Left-Click with triangle once and then once again to double-jump. Press enter to continue.");
                break;
            }
            case "Pattern3.2Tip":
            {
                tutorialPauseScript.ShowTip("Right-Click to match color to color gate. Press enter to continue.");
                break;
            }
            default:
            {
                break;
            }
        }
    }
}
