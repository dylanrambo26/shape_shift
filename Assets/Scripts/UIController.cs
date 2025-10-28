using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{   
    //UI Text
    [SerializeField] private GameObject totalAttemptsObject;
    [SerializeField] private GameObject levelCompleteObject;
    
    //Medal sprite objects
    [SerializeField] private GameObject platinumMedal;
    [SerializeField] private GameObject goldMedal;
    [SerializeField] private GameObject silverMedal;
    [SerializeField] private GameObject bronzeMedal;

    //Return to start menu button
    [SerializeField] private GameObject returnToStartButton;
    private GameObject medalsParent;

    private TextMeshProUGUI totalAttemptsText;
    
    private GameController gameController;
    
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        totalAttemptsText = totalAttemptsObject.GetComponent<TextMeshProUGUI>();
    }
    
    //Change the attempts text to reflect the current number of attempts
    public void UpdateAttempts()
    {
        totalAttemptsText.text = "Total Attempts: " + gameController.TotalAttempts;
    }
    
    //Show the level complete text when it is finished. Pause movement in other scripts.
    public void ShowLevelComplete()
    {
        levelCompleteObject.SetActive(true);
        totalAttemptsObject.SetActive(false);

        MoveLeft.isPaused = true;
        BackgroundScroller.isPaused = true;
        
        returnToStartButton.SetActive(true);
    }

    //Show the correct medal based on number of attempts. Platinum for 1, gold for 2-4, silver for 5-9, and bronze for 10+
    public void ShowMedal()
    {
        if (gameController.TotalAttempts == 1)
        {
            platinumMedal.SetActive(true);
        }
        else if (gameController.TotalAttempts > 1 && gameController.TotalAttempts <= 4)
        {   
            goldMedal.SetActive(true);
        }
        else if (gameController.TotalAttempts > 4 && gameController.TotalAttempts <= 9)
        {
            silverMedal.SetActive(true);
        }
        else if (gameController.TotalAttempts > 9)
        {
            bronzeMedal.SetActive(true);
        }
    }

    //Go back to start menu if the return to start menu button is pressed. Unpause Movement.
    public void ReturnToStartMenu()
    {
        MoveLeft.isPaused = false;
        BackgroundScroller.isPaused = false;
        SceneManager.LoadScene("Title_Screen");
    }
}
