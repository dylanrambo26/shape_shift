using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{   
    [SerializeField] private GameObject totalAttemptsObject;
    [SerializeField] private GameObject levelCompleteObject;
    
    [SerializeField] private GameObject platinumMedal;
    [SerializeField] private GameObject goldMedal;
    [SerializeField] private GameObject silverMedal;
    [SerializeField] private GameObject bronzeMedal;
    private GameObject medalsParent;

    private TextMeshProUGUI totalAttemptsText;
    //private TextMeshProUGUI levelCompleteText;
    private GameController _gameController;
    // Start is called before the first frame update
    void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        totalAttemptsText = totalAttemptsObject.GetComponent<TextMeshProUGUI>();
        // levelCompleteText = levelCompleteObject.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateAttempts()
    {
        totalAttemptsText.text = "Total Attempts: " + _gameController.TotalAttempts;
    }

    public void ShowLevelComplete()
    {
        levelCompleteObject.SetActive(true);
        totalAttemptsObject.SetActive(false);
    }

    public void ShowMedal()
    {
        if (_gameController.TotalAttempts == 1)
        {
            platinumMedal.SetActive(true);
        }
        else if (_gameController.TotalAttempts > 1 && _gameController.TotalAttempts <= 4)
        {   
            goldMedal.SetActive(true);
        }
        else if (_gameController.TotalAttempts > 4 && _gameController.TotalAttempts <= 9)
        {
            silverMedal.SetActive(true);
        }
        else if (_gameController.TotalAttempts > 9)
        {
            bronzeMedal.SetActive(true);
        }
    }

    public void ReturnToStartMenu()
    {
        SceneManager.LoadScene("Title_Screen");
    }
}
