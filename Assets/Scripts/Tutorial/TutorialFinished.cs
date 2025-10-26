using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.UI;
using UnityEngine.SceneManagement;

public class TutorialFinished : MonoBehaviour
{
    [SerializeField] private GameObject totalAttemptsObject;
    [SerializeField] private GameObject tutorialCompleteObject;

    private TextMeshProUGUI totalAttemptsText;
    //private TextMeshProUGUI levelCompleteText;
    private TutorialController tutorialController;
    // Start is called before the first frame update
    void Start()
    {
        tutorialController = GameObject.FindGameObjectWithTag("TutorialController").GetComponent<TutorialController>();
        totalAttemptsText = totalAttemptsObject.GetComponent<TextMeshProUGUI>();
        // levelCompleteText = levelCompleteObject.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateTutorialAttempts()
    {
        totalAttemptsText.text = "Total Attempts: " + tutorialController.TotalAttempts;
    }

    public void ShowTutorialComplete()
    {
        tutorialCompleteObject.SetActive(true);
        totalAttemptsObject.SetActive(false);
    }

    public void ReturnToStartMenu()
    {
        SceneManager.LoadScene("Title_Screen");
    }
}
