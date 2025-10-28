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

    private AudioSource playerAudio;
    [SerializeField] private AudioClip tutorialCompleteSound;
    
    void Start()
    {
        tutorialController = GameObject.FindGameObjectWithTag("TutorialController").GetComponent<TutorialController>();
        totalAttemptsText = totalAttemptsObject.GetComponent<TextMeshProUGUI>();

        playerAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
    }

    //Update the UI with the correct number of attempts from the tutorial controller
    public void UpdateTutorialAttempts()
    {
        totalAttemptsText.text = "Total Attempts: " + tutorialController.TotalAttempts;
    }
    
    //Show the tutorial complete text and return to start menu button when finished. Pause movement in other scripts
    public void ShowTutorialComplete()
    {
        playerAudio.PlayOneShot(tutorialCompleteSound, 1f);
        tutorialCompleteObject.SetActive(true);
        totalAttemptsObject.SetActive(false);

        MoveLeft.isPaused = true;
        BackgroundScroller.isPaused = true;
    }
    //Go back to start menu if the return to start menu button is pressed. Unpause movement.
    public void ReturnToStartMenu()
    {
        MoveLeft.isPaused = false;
        BackgroundScroller.isPaused = false;
        SceneManager.LoadScene("Title_Screen");
    }
}
