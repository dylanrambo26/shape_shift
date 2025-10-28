using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.UI;
using UnityEngine.SceneManagement;

public class TutorialFinished : MonoBehaviour
{
    //UI text
    [SerializeField] private GameObject totalAttemptsObject;
    [SerializeField] private GameObject tutorialCompleteObject;
    
    private TextMeshProUGUI totalAttemptsText;
    private TutorialController tutorialController;
    void Start()
    {
        tutorialController = GameObject.FindGameObjectWithTag("TutorialController").GetComponent<TutorialController>();
        totalAttemptsText = totalAttemptsObject.GetComponent<TextMeshProUGUI>();
    }

    //Update the UI with the correct number of attempts from the tutorial controller
    public void UpdateTutorialAttempts()
    {
        totalAttemptsText.text = "Total Attempts: " + tutorialController.TotalAttempts;
    }
    
    //Show the tutorial complete text and return to start menu button when finished. Pause movement in other scripts
    public void ShowTutorialComplete()
    {
        tutorialCompleteObject.SetActive(true);
        totalAttemptsObject.SetActive(false);

        MoveLeft.isPaused = true;
        BackgroundScroller.isPaused = true;
    }
    //Go back to start menu if the return to start menu button is pressed.
    public void ReturnToStartMenu()
    {
        SceneManager.LoadScene("Title_Screen");
    }
}
